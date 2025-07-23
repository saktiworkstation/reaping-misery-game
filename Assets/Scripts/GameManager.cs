using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public InventorySystem inventorySystem;
    public InventoryUI inventoryUI;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            Destroy(CK);
            Destroy(BGM);
            Destroy(MControl);
            Destroy(inventorySystem.gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Pastikan inventorySystem sudah di-assign sebelum digunakan
        if (inventorySystem == null)
        {
            inventorySystem = FindFirstObjectByType<InventorySystem>(); // Coba cari otomatis jika belum di-assign
            if (inventorySystem == null)
            {
                Debug.LogError("InventorySystem tidak ditemukan atau belum di-assign di GameManager!");
            }
        }
    }

    private void Start()
    {
        ads.ShowBanner();
        if (experience <= 400)
        {
            adsText.text = "Watch Ads +25 pesos";
        }
        else
        {
            adsText.text = "Watch Ads +100 pesos";
        }
    }

    // Ressources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // Refrences
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public Animator deathMenuAnim;
    public GameObject hud;
    public GameObject menu;
    public GameObject CK;
    public GameObject BGM;
    public GameObject MControl;
    public AdsManager ads;
    public Text adsText;

    // logic
    public int pesos;
    public int experience;

    // Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        if(weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if(pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    // Hitpoint Bar
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxhitpoint;
        hitpointBar.localScale = new Vector3(ratio, 1, 1);
    }

    private void Update()
    {
        //GetCurrentLevel();
        //SaveState();
    }

    // Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) // Max Level
                return r;
        }

        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while(r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }
    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();
    }
    public void OnLevelUp()
    {
        player.OnLevelUp();
        OnHitpointChange();
    }

    // Ads Management
    public void GrantAdsPresents()
    {
        ads.PlayRewardedAds(OnRewardedAdSuccess);
    }

    void OnRewardedAdSuccess()
    {
        if(experience <= 400)
        {
            pesos += 25;
        }
        else
        {
            pesos += 100;
        }
        
    }

    // ON Scene Loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        // Pastikan player tidak null sebelum mengakses transform
        if (player != null && GameObject.Find("Spawn") != null)
        {
            player.transform.position = GameObject.Find("Spawn").transform.position;
        }
        else
        {
            Debug.LogWarning("Player atau Spawn point tidak ditemukan saat OnSceneLoaded.");
        }

        // Pastikan CheckPoint.instance tidak null
        if (CheckPoint.instance != null)
        {
            CheckPoint.instance.ChangeCKPoint();
        }
        else
        {
            Debug.LogWarning("CheckPoint.instance tidak ditemukan.");
        }
    }

    // Death Menu and Rrespawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start"); // Pertimbangkan untuk tidak hardcode nama scene
        if (player != null) player.Respawn();
        if (ads != null) ads.PlayAd();

        GameObject playerObject = GameObject.Find("Player");
        if (playerObject == null)
        {
            Debug.Log("Player hilang setelah respawn.");
        }
    }

    // Save state
    /*
     * INT preferedSkin
     * INT pesos
     * INT experience
     * INT weaponLevel
     */
    public void SaveState()
    {
        if (inventorySystem == null)
        {
            Debug.LogError("InventorySystem tidak ada, tidak bisa menyimpan data inventaris.");
            return;
        }

        string s = "";

        s += "0" + "|"; // preferedSkin (asumsi selalu 0 untuk saat ini)
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString() + "|"; // Tambahkan pemisah setelah weaponLevel
        s += inventorySystem.SerializeInventory(); // Data inventaris, tidak perlu pemisah di akhir

        PlayerPrefs.SetString("SaveState", s);
        Debug.Log("Game State Saved with Inventory: " + s);
        //ShowText("Game hasbeen loaded!", 25, Color.yellow, player.transform.position, Vector3.up * 50, 1.5f);
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState; // Penting untuk unsubscribe agar tidak dipanggil berkali-kali

        if (!PlayerPrefs.HasKey("SaveState"))
        {
            Debug.Log("Tidak ada save state ditemukan.");
            // Jika tidak ada save state, mungkin kita ingin menginisialisasi inventaris ke keadaan default
            if (inventorySystem != null)
            {
                inventorySystem.DeserializeInventory(null); // Kosongkan inventaris
                                                            // Jika ada UI inventaris, refresh di sini
                var invUI = FindFirstObjectByType<InventoryUI>(); // Asumsi ada InventoryUI
                if (invUI != null) invUI.RefreshUI();
            }
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Validasi jumlah data
        if (data.Length < 4) // Minimal harus ada data sampai weaponLevel
        {
            Debug.LogError("Format SaveState tidak valid atau data kurang. Data: " + PlayerPrefs.GetString("SaveState"));
            // Reset ke default jika data korup
            if (inventorySystem != null)
            {
                inventorySystem.DeserializeInventory(null);
                var invUI = FindFirstObjectByType<InventoryUI>();
                if (invUI != null) invUI.RefreshUI();
            }
            return;
        }

        // preferedSkin (data[0]) - tidak diimplementasikan sepenuhnya di kode Anda
        // pesos = int.Parse(data[1]);
        // experience = int.Parse(data[2]);
        // weapon.SetWeaponLevel(int.Parse(data[3]));

        // Parsing dengan TryParse untuk keamanan lebih
        if (int.TryParse(data[1], out int loadedPesos)) pesos = loadedPesos;
        else Debug.LogWarning("Gagal parse Pesos dari save data.");

        if (int.TryParse(data[2], out int loadedExperience)) experience = loadedExperience;
        else Debug.LogWarning("Gagal parse Experience dari save data.");

        if (player != null && GetCurrentLevel() != 1) // Pastikan player ada
            player.SetLevel(GetCurrentLevel());
        else if (player == null)
            Debug.LogWarning("Player null saat LoadState, tidak bisa SetLevel.");


        if (weapon != null && int.TryParse(data[3], out int loadedWeaponLevel)) // Pastikan weapon ada
            weapon.SetWeaponLevel(loadedWeaponLevel);
        else if (weapon == null)
            Debug.LogWarning("Weapon null saat LoadState, tidak bisa SetWeaponLevel.");
        else
            Debug.LogWarning("Gagal parse WeaponLevel dari save data.");


        // Load Inventory Data (data[4])
        if (inventorySystem != null)
        {
            if (data.Length > 4) // Cek apakah data inventaris ada
            {
                inventorySystem.DeserializeInventory(data[4]);
            }
            else
            {
                Debug.LogWarning("Data inventaris tidak ditemukan di save state. Menggunakan inventaris kosong.");
                inventorySystem.DeserializeInventory(null); // Kosongkan inventaris jika tidak ada datanya
            }
            // Jika ada UI Inventaris, panggil refresh di sini
            // Misalnya: InventoryUI.instance.RefreshUI();
            // Atau cara yang lebih umum:
            if (inventoryUI != null)
            {
                inventoryUI.RefreshUI();
            }
        }
        else
        {
            Debug.LogError("InventorySystem tidak ada, tidak bisa memuat data inventaris.");
        }
        Debug.Log("Game State Loaded.");
    }
    public void DoExitGame()
    {
        SaveState();
        Application.Quit();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Debug.Log("Application is paused. Saving state.");
            SaveState(); // PENTING untuk mobile!
        }
        else
        {
            Debug.Log("Application has resumed.");
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        SaveState(); // Panggil SaveState di sini
    }
}

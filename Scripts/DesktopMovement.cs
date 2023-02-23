using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesktopMovement : MonoBehaviour
{
    public Camera vision;
    public Camera onHand;
    public float mouseSen = 100;
    public float speed = 10;
    public float gravity = -9.8f;
    public float audioSpeed = 1.2f;
    public float interactionRange = 1;
    public Menu menu;

    private float xRotation = 0;
    private CharacterController controller;
    private Vector3 velocity;
    public AudioSource audioSource;
    private bool footstepPlay = false;

    private GameObject door;
    public GameObject pauseMenu;
    public GameObject achievementPage;
    public bool pauseIsOn = false;

    public GameObject upgradeBook;
    public bool upgradeIsOn = false;
    public Text moneyText;

    public GameObject guideBook;
    public bool guideIsOn = false;

    public AudioSource chachingSource;

    public AudioClip panSound;
    public AudioClip plateSound;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        audioSource.pitch = audioSpeed;
        //audioSource.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", 1f / audioSpeed);
        door = GameObject.FindGameObjectWithTag("AutoDoor");
        mouseSen = 90 + 180 * PlayerPrefs.GetFloat("sensitive");
    }

    private void Update()
    {
        if (!pauseIsOn && !upgradeIsOn && !guideIsOn)
        {
            CameraControl();
            MovementControl();
            Interaction();
            //audioSource.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseIsOn)
            {
                pauseMenu.SetActive(true);
                pauseIsOn = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                pauseMenu.SetActive(false);
                achievementPage.SetActive(false);
                pauseIsOn = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private void CameraControl()
    {
        float x = Input.GetAxis("Mouse X") * mouseSen * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * mouseSen * Time.deltaTime;

        xRotation -= y;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        vision.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        onHand.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * x);
    }

    private void MovementControl()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(speed * Time.deltaTime * move);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (x != 0 || z != 0)
        {
            if (!footstepPlay)
            {
                audioSource.Play();
                footstepPlay = true;
            }
        }
        else
        {
            audioSource.Stop();
            footstepPlay = false;
        }

        if (Mathf.Abs(door.transform.position.x - transform.position.x) < 2 && Mathf.Abs(door.transform.position.z - transform.position.z) < 2)
        {
            door.GetComponent<AutomationDoor>().playerTrigger = true;
        }
        else
        {
            door.GetComponent<AutomationDoor>().playerTrigger = false;
        }
    }

    private void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(vision.transform.position, vision.transform.forward, out RaycastHit hit, interactionRange))
            {
                if (vision.transform.childCount != 1)
                {
                    switch (hit.transform.tag)
                    {
                        case "fridge":
                            {
                                GameObject food = Instantiate(hit.transform.GetComponent<FridgeFood>().food, vision.transform);
                                food.transform.localPosition = new Vector3(0, -0.1f, 0.5f);
                                break;
                            }

                        case "door":
                            {
                                hit.transform.GetComponent<DoorInteract>().OpenClose();
                                break;
                            }

                        case "cut onion":
                        case "food":
                            {
                                GameObject food = hit.transform.gameObject;
                                Destroy(food.GetComponent<Rigidbody>());
                                food.transform.SetParent(vision.transform);
                                food.transform.localPosition = new Vector3(0, -0.1f, 0.5f);
                                food.transform.localEulerAngles = Vector3.zero;
                                if (food.transform.name == "Onion(Clone)") food.layer = 8;
                                else
                                {
                                    for (int i = 0; i < food.transform.childCount; i++)
                                    {
                                        food.transform.GetChild(i).gameObject.layer = 11;
                                    }
                                }
                                break;
                            }

                        case "carrotSlice":
                            {
                                GameObject[] slices = new GameObject[3];
                                for (int i = 0; i < 3; i++)
                                {
                                    slices[i] = hit.transform.parent.GetChild(i).gameObject;
                                    Destroy(slices[i].GetComponent<Rigidbody>());
                                }
                                hit.transform.parent.SetParent(vision.transform);
                                slices[0].transform.localPosition = new Vector3(0, -0.033f, 0.2366f);
                                slices[0].transform.localEulerAngles = new Vector3(126, 0, 0);
                                slices[1].transform.localPosition = new Vector3(0, 0, 0.301f);
                                slices[1].transform.localEulerAngles = new Vector3(123.573f, 0, 0);
                                slices[2].transform.localPosition = new Vector3(0, -0.0525f, 0.1947f);
                                slices[2].transform.localEulerAngles = new Vector3(142.625f, 0, 0);
                                slices[0].transform.parent.localPosition = new Vector3(-0.1f, 0, 0.6f);
                                slices[0].transform.parent.localEulerAngles = new Vector3(0, 60, 0);
                                slices[0].layer = 11;
                                slices[1].layer = 11;
                                slices[2].layer = 11;
                                break;
                            }

                        case "knife":
                            {
                                GameObject knife = Instantiate(hit.transform.GetComponent<KnifeSpawn>().knife, vision.transform);
                                knife.transform.localPosition = new Vector3(0.2f, -0.2f, 0.3f);
                                knife.transform.localScale = Vector3.one / 2;
                                knife.transform.localEulerAngles = new Vector3(0, 90, 0);
                                break;
                            }

                        case "knife object":
                            {
                                GameObject knife = hit.transform.gameObject;
                                Destroy(knife.GetComponent<Rigidbody>());
                                knife.transform.SetParent(vision.transform);
                                knife.transform.localPosition = new Vector3(0.2f, -0.2f, 0.3f);
                                knife.transform.localScale = Vector3.one / 2;
                                knife.transform.localEulerAngles = new Vector3(0, 90, 0);
                                knife.layer = 11;
                                break;
                            }

                        case "pan":
                            {
                                GameObject pan = Instantiate(hit.transform.GetComponent<PanSpawn>().pan, vision.transform);
                                pan.transform.localPosition = new Vector3(0.2f, -0.2f, 0.55f);
                                pan.transform.localScale = Vector3.one / 5;
                                audioSource.PlayOneShot(panSound);
                                break;
                            }

                        case "pan object":
                            {
                                GameObject pan = hit.transform.gameObject;
                                if (pan.GetComponent<Pan>() != null)
                                {
                                    if (pan.GetComponent<Pan>().isOnCooker)
                                        return;
                                }
                                else if (pan.GetComponent<PanWithCarrot>() != null)
                                {
                                    if (pan.GetComponent<PanWithCarrot>().isOnCooker)
                                        return;
                                }
                                else if (pan.GetComponent<PanWithOnion>() != null)
                                {
                                    if (pan.GetComponent<PanWithOnion>().isOnCooker)
                                        return;
                                }
                                pan.transform.SetParent(vision.transform);
                                Destroy(pan.GetComponent<Rigidbody>());
                                pan.transform.localPosition = new Vector3(0.2f, -0.2f, 0.55f);
                                pan.transform.localScale = Vector3.one / 5;
                                pan.transform.localEulerAngles = Vector3.zero;
                                if (pan.GetComponent<PanWithOnion>() != null)
                                    pan.layer = 9;
                                else if (pan.GetComponent<PanWithCarrot>() != null)
                                    pan.layer = 10;
                                else pan.layer = 11;
                                break;
                            }

                        case "plate pile":
                            {
                                GameObject plate = Instantiate(hit.transform.GetComponent<SpawnPlate>().plate, vision.transform);
                                plate.transform.localPosition = new Vector3(0.2f, -0.2f, 0.4f);
                                break;
                            }

                        case "plate":
                            {
                                GameObject plate = hit.transform.gameObject;
                                plate.transform.SetParent(vision.transform);
                                plate.transform.localPosition = new Vector3(0.2f, -0.2f, 0.5f);
                                plate.transform.localEulerAngles = Vector3.zero;
                                Destroy(plate.GetComponent<Rigidbody>());
                                plate.layer = 11;
                                break;
                            }

                        case "dish":
                            {
                                GameObject dish = hit.transform.gameObject;
                                dish.transform.SetParent(vision.transform);
                                dish.transform.localPosition = new Vector3(0.2f, -0.2f, 0.5f);
                                dish.transform.localEulerAngles = Vector3.zero;
                                Destroy(dish.GetComponent<Rigidbody>());
                                dish.layer = 11;
                                break;
                            }

                        case "cup pile":
                            {
                                GameObject cup = Instantiate(hit.transform.GetComponent<CupPile>().cup, vision.transform);
                                cup.transform.localPosition = new Vector3(0.5f, -0.5f, 1);
                                break;
                            }

                        case "cup":
                            {
                                GameObject cup = hit.transform.gameObject;
                                cup.transform.SetParent(vision.transform);
                                cup.layer = 11;
                                cup.transform.localPosition = new Vector3(0.5f, -0.5f, 1);
                                Destroy(cup.GetComponent<Rigidbody>());
                                break;
                            }

                        case "sauce pile":
                            {
                                GameObject sauceDish = Instantiate(hit.transform.gameObject);
                                sauceDish.transform.SetParent(vision.transform);
                                sauceDish.transform.localPosition = new Vector3(0.2f, -0.2f, 0.5f);
                                sauceDish.tag = "sauce Dish";
                                sauceDish.layer = 11;
                                break;
                            }

                        case "sauce Dish":
                        case "sauce plate":
                            {
                                GameObject sauceDish = hit.transform.gameObject;
                                sauceDish.transform.SetParent(vision.transform);
                                sauceDish.layer = 11;
                                sauceDish.transform.localPosition = new Vector3(0.2f, -0.2f, 0.5f);
                                Destroy(sauceDish.GetComponent<Rigidbody>());
                                break;
                            }

                        case "Board":
                            {
                                upgradeBook.SetActive(true);
                                Cursor.lockState = CursorLockMode.None;
                                upgradeIsOn = true;
                                moneyText.color = Color.black;
                                for (int i = 0; i < upgradeBook.GetComponent<UpgradeControl>().starMeter.Length; i++)
                                    upgradeBook.GetComponent<UpgradeControl>().starMeter[i].SetActive(false);
                                audioSource.Stop();
                                break;
                            }

                        case "Book":
                            {
                                guideBook.SetActive(true);
                                guideIsOn = true;
                                Cursor.lockState = CursorLockMode.None;
                                break;
                            }
                    }
                }
                else
                {
                    if (vision.transform.GetChild(0).CompareTag("plate"))
                    {
                        if (hit.transform.CompareTag("cooked food"))
                        {
                            audioSource.PlayOneShot(plateSound);
                            if (!hit.transform.GetComponent<FoodVariety>().haveOnion)
                            {
                                if (!hit.transform.GetComponent<FoodVariety>().haveCarrot)
                                {
                                    switch (hit.transform.gameObject.layer)
                                    {
                                        case 6:
                                            {
                                                Replace(menu.cookedSteak);
                                                Destroy(hit.transform.GetComponent<FoodVariety>().collideWith);
                                                Destroy(hit.transform.gameObject);
                                                break;
                                            }

                                        case 7:
                                            {
                                                Replace(menu.cookedFish);
                                                Destroy(hit.transform.GetComponent<FoodVariety>().collideWith);
                                                Destroy(hit.transform.gameObject);
                                                break;
                                            }
                                    }
                                }
                                else
                                {
                                    switch (hit.transform.gameObject.layer)
                                    {
                                        case 6:
                                            {
                                                Replace(menu.cookedSteakWithCarrot);
                                                Destroy(hit.transform.GetComponent<FoodVariety>().collideWith);
                                                Destroy(hit.transform.gameObject);
                                                break;
                                            }
                                        case 7:
                                            {
                                                Replace(menu.cookedFishWithCarrot);
                                                Destroy(hit.transform.GetComponent<FoodVariety>().collideWith);
                                                Destroy(hit.transform.gameObject);
                                                break;
                                            }
                                    }
                                }
                            }
                            else
                            {
                                if (!hit.transform.GetComponent<FoodVariety>().haveCarrot)
                                {
                                    switch (hit.transform.gameObject.layer)
                                    {
                                        case 6:
                                            {
                                                Replace(menu.cookedSteakWithOnion);
                                                Destroy(hit.transform.GetComponent<FoodVariety>().collideWith);
                                                Destroy(hit.transform.gameObject);
                                                break;
                                            }

                                        case 7:
                                            {
                                                Replace(menu.cookedFishWithOnion);
                                                Destroy(hit.transform.GetComponent<FoodVariety>().collideWith);
                                                Destroy(hit.transform.gameObject);
                                                break;
                                            }
                                    }
                                }
                                else
                                {
                                    switch (hit.transform.gameObject.layer)
                                    {
                                        case 6:
                                            {
                                                Replace(menu.cookedSteakWithOnionCarrot);
                                                Destroy(hit.transform.GetComponent<FoodVariety>().collideWith);
                                                Destroy(hit.transform.gameObject);
                                                break;
                                            }

                                        case 7:
                                            {
                                                Replace(menu.cookedFishWithOnionCarrot);
                                                Destroy(hit.transform.GetComponent<FoodVariety>().collideWith);
                                                Destroy(hit.transform.gameObject);
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                    }
                    else if (vision.transform.GetChild(0).CompareTag("knife object"))
                    {
                        if (hit.transform.GetComponent<CutOnion>() != null)
                        {
                            GameObject onion = hit.transform.gameObject;
                            onion.transform.GetChild(0).GetComponent<Renderer>().material = onion.GetComponent<CutOnion>().cutOnion;
                            onion.transform.GetChild(1).GetComponent<Renderer>().material = onion.GetComponent<CutOnion>().cutOnion;
                            onion.tag = "cut onion";
                            onion.GetComponent<CutOnion>().isCut = true;
                        }
                        else if (hit.transform.GetComponent<CutCarrot>() != null)
                        {
                            GameObject carrot = hit.transform.gameObject;
                            Vector3 position = carrot.transform.localPosition;
                            Vector3 angles = carrot.transform.eulerAngles;
                            Vector3 scale = carrot.transform.localScale;
                            Destroy(carrot);
                            GameObject carrot_Slice = Instantiate(carrot.GetComponent<CutCarrot>().cutCarrot);
                            carrot_Slice.transform.position = position;
                            carrot_Slice.transform.eulerAngles = angles;
                            carrot_Slice.transform.localScale = scale;
                            carrot_Slice.transform.GetChild(0).GetComponent<CutCarrot>().isCut = true;
                        }
                        else DropItem();
                    }
                    else if (vision.transform.GetChild(0).CompareTag("cup"))
                    {
                        if (hit.transform.gameObject.CompareTag("water cooler"))
                        {
                            vision.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        }
                    }
                    else if (vision.transform.GetChild(0).CompareTag("sauce Dish"))
                    {
                        if (hit.transform.gameObject.CompareTag("sauce"))
                        {
                            vision.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        }
                    }
                    else if (vision.transform.GetChild(0).CompareTag("dish"))
                    {
                        if (hit.transform.gameObject.CompareTag("customer"))
                        {
                            int dishNumber = vision.transform.GetChild(0).GetComponent<OrderingNumber>().orderNumber;
                            int customerOrder = hit.transform.GetComponent<Customers>().orderDish;
                            if (customerOrder == dishNumber)
                            {
                                GameObject dish = vision.transform.GetChild(0).gameObject;
                                dish.layer = 0;
                                for (int i = 0; i < dish.transform.childCount; i++)
                                {
                                    if (dish.transform.GetChild(i).childCount != 0)
                                        for (int j = 0; j < dish.transform.GetChild(i).childCount; j++)
                                            dish.transform.GetChild(i).GetChild(j).gameObject.layer = 0;
                                    else dish.transform.GetChild(i).gameObject.layer = 0;
                                }
                                dish.transform.SetParent(hit.transform);
                                dish.transform.localPosition = new Vector3(0, 1, 0.3f);
                                dish.transform.localEulerAngles = Vector3.zero;
                                StartCoroutine(hit.transform.GetComponent<Customers>().Eat());
                                MoneyControl.money += dish.GetComponent<OrderingNumber>().price;
                                chachingSource.Play();
                                DataManager.instance.SaveData();
                            }
                        }
                        else DropItem();
                    }
                    else DropItem();
                }
            }
            else if (vision.transform.childCount == 1)
            {
                DropItem();
            }
        }
    }

    private void DropItem()
    {
        GameObject food = vision.transform.GetChild(0).gameObject;
        food.layer = 0;
        food.transform.SetParent(null);
        if (food.CompareTag("food") && food.layer != 8)
        {
            food.AddComponent<Rigidbody>();
            for (int i = 0; i < food.transform.childCount; i++)
            {
                food.transform.GetChild(i).gameObject.layer = 0;
            }
        }
        else if (food.CompareTag("carrotSlice"))
        {
            for (int i = 0; i < food.transform.childCount; i++)
            {
                food.transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
                food.transform.GetChild(i).gameObject.layer = 0;
            }
        }
        else food.AddComponent<Rigidbody>();
    }

    private void Replace(GameObject target)
    {
        GameObject plate = vision.transform.GetChild(0).gameObject;
        Vector3 position = plate.transform.localPosition;
        Destroy(plate);
        GameObject foodPlate = Instantiate(target, vision.transform);
        foodPlate.transform.localPosition = position;
        if (foodPlate.GetComponent<Rigidbody>() != null)
            Destroy(foodPlate.GetComponent<Rigidbody>());
    }
}

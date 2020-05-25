using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    private int game_no;
    public Image main_charater_image;
    public Sprite[] main_charater_image_array;
    public Text CardStatText;
    private Sprite tempSP; // shuffle temp
    private GameObject tempGO; // shuffle temp
    private int game_point = 0;
    private int per_game_point = 0;
    public Text game_report;
    private int game_step = 0; // 0: 초기화 , 1: 게임시작 (간식의 위치 기억), 
    private int game_difficult = 1; // 사용자 게임 난이도  
    private int shuffle_count = 1;
    /**
     * 0: 오답 이미지, 1: 정답 이미지
     */
    public Sprite[] snack_sprite_array;
    public Sprite[] flower_sprite_array;
    public Sprite[] hat_sprite_array;
    // 게임 카드 sprites 
    [SerializeField]
    private Sprite[] game_card_sprites;
    // 게임 카드 이미지
    public Image[] game_card_images;

    public GameObject[] block_image_array;
    
    private IEnumerator STEP;
    private Coroutine co;

    private float card_width = 160.0f;
    private float card_height = 160.0f;

    private int click_count = 0;
    private int click_count_max = 2;

    private bool block_image_stat = false;

    private GameObject[] cards;

    public ProgressBar progressBar;
    
    private void Awake()
    {
        game_no = Globals.game_no;
        game_difficult = Globals.game_difficult;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (game_difficult == 1) shuffle_count = 2;
        else if (game_difficult == 2) shuffle_count = Random.Range(3,5);
        else if (game_difficult == 3) shuffle_count = Random.Range(4,6);
        game_card_sprites = new Sprite[3];
        init_card_stat_text(game_no);
        init_main_character_image(game_no);
        init_game_card_images(game_no);
        update_game_point(game_point);
        STEP = CO_STEP();
        StartCoroutine(STEP);
    }

    // Update is called once per frame
    IEnumerator CO_STEP()
    {
        yield return new WaitForSeconds(3.0f);
        init_card_stat_text(game_no);
        set_card_image_alpha_all(0.0f);
        block_image_active(true);  
        yield return new WaitForSeconds(1.0f);
        // shuffle animation
        cards = GameObject.FindGameObjectsWithTag("CardImage");
        co = StartCoroutine(ShuffleAnim(cards , shuffle_count));    
    }
    
    IEnumerator ShuffleAnim(GameObject[] cards, int shuffle_count)
    {
        for (int i = 0; i < shuffle_count; i++) 
        {
            print("shuffle : " + i );
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            int rnd;
            int rnd2;
            while (true)
            {
                rnd= Random.Range(0, cards.Length);
                rnd2= Random.Range(0, cards.Length);
                if(rnd != rnd2) break;
            }
            RectTransform rt = cards[rnd].GetComponent<RectTransform>();
            Vector2 targetPos = cards[rnd2].GetComponent<RectTransform>().localPosition;
            RectTransform rt2 = cards[rnd2].GetComponent<RectTransform>();
            Vector2 targetPos2 = cards[rnd].GetComponent<RectTransform>().localPosition;
            float step = 0;
            float delay = 0.1f;
            while (step < 0.5f)
            {
                // print(step);
                rt.localPosition = Vector2.Lerp(rt.localPosition, targetPos, step += Time.deltaTime * delay);
                rt2.localPosition = Vector2.Lerp(rt2.localPosition, targetPos2, step += Time.deltaTime * delay);
                yield return new WaitForEndOfFrame();
            }    
        }
        update_card_stat_text();
        //progressBar.StartCountTime();   
        block_image_stat = true;
        StopCoroutine(co);
        StartCoroutine(STEP);
    }
    
    public void block_image_active(bool set)
    {
        for (int i = 0; i < block_image_array.Length; i++)
        {
            block_image_array[i].SetActive(set);      
        }
    }

    private void set_card_image_alpha_all(float alpha)
    {
        cards = GameObject.FindGameObjectsWithTag("CardImage");
        for (int i = 0; i < cards.Length; i++)
        {
            Color a = cards[i].GetComponent<Image>().color;
            a.a = alpha;
            cards[i].GetComponent<Image>().color = a;
        }
    }
    
    private void set_card_image_alpha(GameObject obj, float alpha)
    {
        Color a = obj.GetComponent<Image>().color;
        a.a = alpha;
        obj.GetComponent<Image>().color = a;
    }
    
    
    void update_card_stat_text()
    {
        String text = "";
        if (game_no == 1) text = "간식";
        else if (game_no == 2) text = "꽃";
        else if (game_no == 3) text = "모자";
        CardStatText.text = text + "은 어디에 있을까요?";
    }

    void show_car_stat_point(int per_game_point)
    {
        String text = "";
        if (game_no == 1) text = "간식";
        else if (game_no == 2) text = "꽃";
        else if (game_no == 3) text = "모자";
        CardStatText.text = per_game_point + "개의 "+text+"을(를) 찾았습니다!";
    }
    

    void init_main_character_image(int game_no)
    {
        for (int i = 0; i < main_charater_image_array.Length; i++)
        {
            if (i + 1 == game_no)
            {
                main_charater_image.GetComponent<Image>().sprite = main_charater_image_array[i];    
            }
        }
    }

    void init_card_stat_text(int game_no)
    {
        String text = "";
        if (game_no == 1) text = "간식";
        else if (game_no == 2) text = "꽃";
        else if (game_no == 3) text = "모자";
        CardStatText.text = text + " 위치를 기억하세요!";
            
    }

    void init_game_card_images(int game_no)
    {
        if (game_no == 1)
        {
            game_card_sprites[0] = snack_sprite_array[0];
            game_card_sprites[1] = snack_sprite_array[1];
            game_card_sprites[2] = snack_sprite_array[1];
        }
        else if(game_no == 2)
        {
            game_card_sprites[0] = flower_sprite_array[0];
            game_card_sprites[1] = flower_sprite_array[1];
            game_card_sprites[2] = flower_sprite_array[1];
        }else if (game_no == 3)
        {
            game_card_sprites[0] = hat_sprite_array[0];
            game_card_sprites[1] = hat_sprite_array[1];
            game_card_sprites[2] = hat_sprite_array[1];
        }
        Shuffle_sprite();
        for (int i = 0; i < game_card_images.Length; i++)
        {
            game_card_images[i].GetComponent<Image>().sprite = game_card_sprites[i];
        }
    }
    
    public void Shuffle_sprite() {
        for (int i = 0; i < game_card_sprites.Length; i++) {
            int rnd = Random.Range(i, game_card_sprites.Length);
            tempSP = game_card_sprites[rnd];
            game_card_sprites[rnd] = game_card_sprites[i];
            game_card_sprites[i] = tempSP;
        }
    }

    void update_game_point(int game_point)
    {
        String text = "";
        if (game_no == 1) text = "간식";
        else if (game_no == 2) text = "꽃";
        else if (game_no == 3) text = "모자";
        game_report.text = "찾은 "+text+" : " + game_point + "개";
    }

    public void StartShuffle()
    {
        GameObject[] cards = GameObject.FindGameObjectsWithTag("CardImage");
        co = StartCoroutine(ShuffleAnim(cards , shuffle_count));    
    }

    // 블록 이미지 선택 
    public void click_block_image(GameObject obj)
    {
        if (block_image_stat)
        {
            set_card_image_alpha(obj.transform.parent.gameObject, 255.0f);
            obj.SetActive(false);
            set_game_point(obj);
            set_click_count();    
        }
    }

    // 게임 포인트 취합
    private void set_game_point(GameObject obj)
    {
        string ans = "o";
        if (obj.transform.parent.GetComponent<Image>().sprite.name.Contains(ans))
        {
            print(obj.transform.parent.GetComponent<Image>().sprite.name);
            this.game_point++;
            per_game_point++;
            update_game_point(this.game_point);
        }
    }

    // 클릭 카운트 
    private void set_click_count()
    {
        click_count++;
        if (click_count == click_count_max)
        {
            // progressBar.StopCountTime();
            show_car_stat_point(per_game_point);
            per_game_point = 0;
            set_card_image_alpha_all(255.0f);
            block_image_active(false);
            block_image_stat = false;
            STEP = CO_STEP();
            StartCoroutine(STEP);
            click_count = 0;
        }    
    }
    
    // 게임 종료 
    public void finish_game()
    {
        set_card_image_alpha_all(255.0f);
        block_image_active(false);
        CardStatText.text = "게임이 종료되었습니다.";
        StopCoroutine(co);
        StopCoroutine(STEP);
    }

    // 게임 재시작
    public void Restart_gaem()
    {
        StopCoroutine(co);
        StartCoroutine(STEP);
    }    
}//.class

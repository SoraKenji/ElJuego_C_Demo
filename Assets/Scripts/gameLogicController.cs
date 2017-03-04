using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameLogicController : MonoBehaviour
{
    enum GameState { playing, pause, game_over, WIN };
    GameState gState; 
	private static gameLogicController _instance;
    public static gameLogicController Instance { get { return _instance; } }
    public controller player;
	public int puntaje = 0;
	public Text Tpunt, TduracionJuego, TgameOver, TWin;
    public float DuracionJuegoEnSeg;
    float durJuego;
    bool TiempoMinimo = false;
    float Trest;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

	void Start(){
        ReiniciarJuego();
	}

    void ReiniciarJuego(){
        gState = GameState.playing;
        Instance.player = player;
        player.OnDeath += gameOver;
		Instance.Tpunt = Tpunt;
        durJuego = Time.timeSinceLevelLoad;
        Trest = DuracionJuegoEnSeg;
    }
    void Update()
    {
        if(gState == GameState.playing){
            if(Trest > 0){
                Trest = DuracionJuegoEnSeg - Time.timeSinceLevelLoad;
                TduracionJuego.text = Trest.ToString();
                if(Trest <= 10 && !TiempoMinimo){
                    TduracionJuego.color = Color.red;
                    TiempoMinimo = true;
                }
            }else{
                Time.timeScale = 0;
                gameOver();
            }
        }
        else if (gState == GameState.game_over){
            TduracionJuego.text = "0";
            if(Input.GetKeyDown(KeyCode.Space)){
                ReiniciarJuego();
                SceneManager.LoadScene("Prueba");
            }
        }
        else if (gState == GameState.WIN){
            TduracionJuego.text = "0";
            if(Input.GetKeyDown(KeyCode.Space)){
                ReiniciarJuego();
                SceneManager.LoadScene("Menu");
            }
        }
    }
    void gameOver(){
        TgameOver.gameObject.SetActive(true);
        TgameOver.transform.parent.gameObject.SetActive(true);
        gState = GameState.game_over;
    }

    public void Terminar(){
        Time.timeScale = 0;
        gState = GameState.WIN;
        TWin.gameObject.SetActive(true);
        TWin.transform.parent.gameObject.SetActive(true);

    }
}
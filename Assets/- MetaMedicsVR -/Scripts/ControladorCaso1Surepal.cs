using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControladorCaso1Surepal : MonoBehaviour
{

    public TextMeshProUGUI textoExplicacíon;

    public string[] textosDePasoEnOrden;

    public Animator panelConsejo;

    [Header("Paso Zero ")]
    public Animator muestraSurepals;

    public GameObject paqueteEstucheInteraccion;

    public GameObject[] estuchesFakeAApagar;

    public Animator[] surepalErroneosARetirar;

    public GameObject panelMuyBien;

    public GameObject[] parentPasos;

    [Header("Primer Paso")]
    public Animator neveraAnimator;
    public Animator pomoNevera;
    public GameObject dragAndDropAbrirNeveraLogica;

    public GameObject dragAndDropSacarSurepalLogica;

    public GameObject surepalEnNevera;

    public GameObject surepalParaAnimacionFueraDeNevera;

    [Header("Segundo Paso")]
    public GameObject estucheparaPasoDos;
    public Transform agujaEnMesaPasoDos;
    public Animator mesaPupitre;
    public GameObject cartuchoEnEstucheEnMesa;


    public GameObject sliderLogicaSacarAguja;


    public GameObject sliderLogicaAgujaEnMesa;

 
    public GameObject sliderLogicaCartuchoALaMesa;


    public GameObject sliderLogicaOcultadorALaMesa;

    public GameObject[] p2_agujas;
    public GameObject p2_agujaSeleccionada;
    public GameObject p2_cubreAguja;
    public GameObject p2_cartucho;

    [Header("Tercer Paso")]
    public GameObject SM_SurepalParaPaso3;
    public GameObject SM_SurepalEnAnimacionAborrarParaPaso3;
    public GameObject SM_SurepalEstuche;

    public GameObject sliderLogicaSacarSurepal;
 
    public GameObject sliderLogicaEnSurepalSacarTapon;

    public GameObject capuchonConDraggable;

    public GameObject capuchonParaVolverALaMesa;

  
    public GameObject sliderLogicaTaponAEstuche;

    public Transform anilloDeCierreSurePalPaso3;



    public GameObject sliderLogicoDesenrroscaAnillo;

    public GameObject anilloDeCierrePaso3Sacar;
    public GameObject anilloDeCierrePaso3Visual;


    public GameObject SM_SurepalParaPaso3FINAl;

    public GameObject sliderLogicoSueltaRoscaAnillo;

    public GameObject canvasEtiquetas;

    [Header("CanvasObjetos")]
    public GameObject parentIndicadorAguja;
    public GameObject parentIndicadorCartucho;
    public GameObject parentIndicadorOcultador;
    public GameObject parentIndicadorAnilloDeCierre;
    public GameObject parentIndicadorTapa;
    public GameObject parentIndicadorSurepal;
    public GameObject parentIndicadorContenedorAgujas;
    public GameObject parentIndicadorToallitasAlcoholicas;

    [Header("CinematicaToallitas")]
    public GameObject toallitasParent;

    public GameObject disposalCan;

    [Header("CambioPaso4")]
    public GameObject piezasSueltasSurepalConEstuche;

    [Header("QuintoPaso")]
    public GameObject linearDraggableLogicaCartucho;


    public GameObject linearDraggableLogicoAnilloeCierre;



    public GameObject linearDraggableLogicaColocaCartucho;


    public GameObject linearDraggableLogicaGIRACartucho;


    public GameObject linearDraggableLogicaClickCartucho;


    [Header("SextoPaso")]
    public GameObject surepalPaso6;
    public GameObject anilloDeCierrePaso6;
    public GameObject cartuchoEnAnilloDECierre;
    public GameObject cartuchoEnAnilloDEABorrar;

    public GameObject linearDraggableLogicaEncajarAnillo;

    public GameObject linearDraggableLogicaAlineaSurepalYCierre;


    public GameObject linearDraggableLogicaRotarAnilloEnSurepal;

    public GameObject SurepalPasoLimpiarPunta;

    [Header("PasoLimpiarPunta")]

    public GameObject linearDraggableLogicaLimpiarPuntaSurepal;
    public GameObject washclothEspecialTirarALaBasura;


    [Header("AbrirAguja")]
    public GameObject agujaAAbrir;

    public GameObject linearDraggableLogicaQuitarPegatina;

    [Header("Meter Aguja")]

    public GameObject linearDraggableLogicaAgujaEnSurepal;

    [Header("Rotar Aguja")]

    public GameObject linearDraggableLogicaRotarAgujaEnSurepal;

    [Header("Sacar Capuchon")]

    public GameObject linearDraggableLogicaSacaCapuchaAgujaEnSurepal;

    [Header("ColocarCubreAgujas")]
    public GameObject CubreAgujasSuelto;
    public GameObject CubreAgujasEnEstucheABorrar;
    public GameObject linearDragElevarCubreAgujas;

    public GameObject linearDraggableLogicaColocaOcultador;

    public GameObject capuchonSuelto;
    public GameObject capuchonABorrar;

    [Header("GirarDosisFinal")]
    public Animator surepalDefinitivoFinal;

    public GameObject linearDraggableLogicaGirarDosis;

    public TextMeshProUGUI textTop;
    public TextMeshProUGUI textMid;
    public TextMeshProUGUI textBot;

    private string topText = " ";
    private float midValue = 0f;
    private float botValue = 0.2f;

    public GameObject NinioParaMedicar;

    [Header("Medicarse")]
    public Animator canvasElegirAnimator;

    public GameObject linearDraggableLogicaLimpiaZona;

    public GameObject linearDraggableLogicaEcharSurepalHaciaAtras;


    public GameObject linearDraggableLogicaSacarProtectorDeAguja;


    public GameObject linearDraggableLogicaPincharSurepal;


    public GameObject linearDraggableLogicaPulsarBotonEnSurepal;

    public GameObject linearDraggableLogicaSacarSurepal;

    public GameObject botonSurepal;

    #region Auxiliares
    public CountDownTimer countDownTimer;

    public void CheckDistance(float value)
    {
        if (value > 0.85f)
        {
            if (!countDownTimer.IsCounting())
            {
                countDownTimer.StartTimer();
                surepalDefinitivoFinal.GetComponentInChildren<Animator>().Rebind();
                surepalDefinitivoFinal.GetComponentInChildren<Animator>().Play("AguantarBotonSurepal");
                surepalDefinitivoFinal.GetComponentInChildren<Animator>().speed = 1;
            }
        }
        else
        {
            if (countDownTimer.IsCounting())
            {
                countDownTimer.StopTimer();
                surepalDefinitivoFinal.GetComponentInChildren<Animator>().speed = 0;
            }
        }
    }


    public void SetTopText(int text)
    {
        textTop.text = text.ToString();
    }
    public void SetMidText(string text)
    {
        textMid.text = text.ToString();
    }
    public void SetBotText(string text)
    {
        textBot.text = text.ToString();
    }

    public void ChangeAndShowConsejo(string consejoTexto)
    {
        panelConsejo.GetComponentInChildren<TextMeshProUGUI>().text = consejoTexto;
        panelConsejo.Play("MostrarConsejo");

    }
    public void TurnOffOutlines(Transform parentGameobject)
    {
        Outline outline = parentGameobject.GetComponent<Outline>();
        if (outline)
        {
            outline.enabled = false;
        }
        for (int i = 0; i < parentGameobject.childCount; i++)
        {
            TurnOffOutlines(parentGameobject.GetChild(i));
        }
    }

    public void TurnOnOutlines(Transform parentGameobject)
    {
        Outline outline = parentGameobject.GetComponent<Outline>();
        if (outline)
        {
            outline.enabled = true;
        }
        for (int i = 0; i < parentGameobject.childCount; i++)
        {
            TurnOnOutlines(parentGameobject.GetChild(i));
        }
    }
    private void Start()
    {
        StartReconocerDispositivo();
    }

    public void CallReplaceSurepalsEnEstuche()
    {
        for (int i = 0; i < estuchesFakeAApagar.Length; i++)
        {
            estuchesFakeAApagar[i].SetActive(false);
        }
        paqueteEstucheInteraccion.SetActive(true);
        textoExplicacíon.text = textosDePasoEnOrden[1];
        ChangeAndShowConsejo("Selecciona tu Surepal");
    }

    public void CallAnimationSurepalCorrect(Animator surepalAnimator)
    {
        surepalAnimator.CrossFade("ClickarSurepalCorrecto", 0.2f);
        surepalErroneosARetirar[0].GetComponent<Collider>().enabled = false;
        surepalErroneosARetirar[1].GetComponent<Collider>().enabled = false;
        surepalAnimator.GetComponent<Collider>().enabled = false;
        for (int i = 0; i < surepalErroneosARetirar.Length; i++)
        {
            surepalErroneosARetirar[i].CrossFade("RetiraSurepal", 0.2f);
        }
        MoveToMousePositionPanelMuyBien();
    }

    public void CallAnimationSurepalIncorrect(Animator surepalAnimator)
    {
        surepalAnimator.CrossFade("ClickarSurepalErroneo", 0.4f);
    }

    public void MoveToMousePositionPanelMuyBien()
    {
        CancelInvoke(nameof(DesactivarPanelMuyBien));
        panelMuyBien.SetActive(true);
        // Obtener la posición del ratón en la pantalla
        Vector2 mousePosition = Input.mousePosition;

        // Convertir la posición del ratón a la posición dentro del Canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            panelMuyBien.transform.parent as RectTransform,
            mousePosition,
            null,
            out Vector2 localPoint);
        // Mover el GameObject a la posición calculada
        panelMuyBien.GetComponent<RectTransform>().anchoredPosition = localPoint;
        panelMuyBien.GetComponentInChildren<Animator>().Play("Pressed");
        panelMuyBien.GetComponentInChildren<AudioSource>().Play();
        Invoke(nameof(DesactivarPanelMuyBien), 0.8f);
    }

    public void DesactivarPanelMuyBien()
    {
        panelMuyBien.SetActive(false);
    }

    #endregion

    #region DEBUG

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 20f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1f;
        }
    }
    #endregion


    #region ReconocerDispositivo
    public void StartReconocerDispositivo()
    {
        paqueteEstucheInteraccion.SetActive(false);
        textoExplicacíon.text = textosDePasoEnOrden[0];
        muestraSurepals.Play("MostrarSurepals"); // la animacion dura 5 secs
        Invoke(nameof(CallReplaceSurepalsEnEstuche), 5);
    }

    public void ReconocerDispositivo()
    {
        textoExplicacíon.text = textosDePasoEnOrden[2];

        for (int i = 0; i < surepalErroneosARetirar.Length; i++)
        {
            surepalErroneosARetirar[i].GetComponent<Clickable>().enabled = false;
        }
        Invoke(nameof(AparecerNevera), 5);
    }

    public void AparecerNevera()
    {
        parentPasos[0].SetActive(false);
        parentPasos[1].SetActive(true);
        Invoke(nameof(EnableSliderVisualNevera), 3);
    }

    public void EnableSliderVisualNevera()
    {
        ChangeAndShowConsejo("Selecciona el asa y arrastra");
    }

    public void BajarPomoNevera()
    {
        pomoNevera.CrossFade("BajaPomoNevera", 0.5f);
    }

    public void SubirPomoNevera()
    {
        pomoNevera.CrossFade("SubePomoNevera", 0.5f);
    }

    public void SlideNeveraTerminado()
    {
        TurnOffOutlines(neveraAnimator.transform);
        MoveToMousePositionPanelMuyBien();
        dragAndDropAbrirNeveraLogica.SetActive(false);
        neveraAnimator.Play("NeveraCloseUp");
        Invoke(nameof(EnableSliderVisualSacarSurepalDeNevera), 1);
    }

    public void EnableSliderVisualSacarSurepalDeNevera()
    {
        dragAndDropSacarSurepalLogica.SetActive(true);
        ChangeAndShowConsejo("Selecciona el estuche y arrastra");
        TurnOnOutlines(surepalEnNevera.transform);
    }

    public void SacarDispositivoDeNevera()
    {
        dragAndDropSacarSurepalLogica.SetActive(false);
        neveraAnimator.Play("NeveraSeCierraYAleja");
        surepalParaAnimacionFueraDeNevera.SetActive(true);
        parentPasos[2].SetActive(true);
        //surepalParaAnimacionFueraDeNevera.transform.parent = parentPasos[2].transform;
        surepalParaAnimacionFueraDeNevera.GetComponent<Animator>().Play("AcomodarEnMesa");
        surepalEnNevera.SetActive(false);
        MoveToMousePositionPanelMuyBien();
        Invoke(nameof(StartSacarPiezas), 2);
        TurnOffOutlines(surepalEnNevera.transform);
        //ANIMACION MESA CON DELAY
    }

    #endregion

    #region SacarPiezas
    public void StartSacarPiezas()
    {
        mesaPupitre.Play("ApareceMesaPupitre");
        Invoke(nameof(SacarAgujas), 4);
    }

    public void SacarAgujas()
    {
        textoExplicacíon.text = textosDePasoEnOrden[3];
        surepalParaAnimacionFueraDeNevera.gameObject.SetActive(false);
        estucheparaPasoDos.gameObject.SetActive(true);

        sliderLogicaSacarAguja.SetActive(true);
        ChangeAndShowConsejo("Saca una aguja del estuche");
    }

    public void AgujasEnElAire()
    {
        textoExplicacíon.text = textosDePasoEnOrden[4];
        TurnOffOutlines(estucheparaPasoDos.transform);
        //TurnOnOutlines();
        //sliderLogicaAgujaEnMesa.SetActive(true);
        estucheparaPasoDos.GetComponent<Animator>().Play("PonerAgujaEnMesa");
        sliderLogicaSacarAguja.SetActive(false);
        ChangeAndShowConsejo("Coloca la aguja en la mesa");
        Invoke(nameof(AuxiliarAutoCompletePonerAgujaEnMesa),5);
    }

    public void AuxiliarAutoCompletePonerAgujaEnMesa() 
    {
        ColocarAgujasEnMesa();
    }

    public void ColocarAgujasEnMesa()
    {
        textoExplicacíon.text = textosDePasoEnOrden[5];
        //TurnOnOutlines();
        //TurnOnOutlines();

        sliderLogicaAgujaEnMesa.SetActive(false);
        TurnOnOutlines(cartuchoEnEstucheEnMesa.transform);
        sliderLogicaCartuchoALaMesa.SetActive(true);
        ChangeAndShowConsejo("Coloca el cartucho sobre la mesa");
        TurnOffOutlines(agujaEnMesaPasoDos.transform);
        //enciende los nuevos sliders 
    }

    public void ColocarCartuchoEnMesa()
    {
        TurnOffOutlines(cartuchoEnEstucheEnMesa.transform);
  
        cartuchoEnEstucheEnMesa.GetComponent<Outline>().enabled = false;
        textoExplicacíon.text = textosDePasoEnOrden[6];
        
        sliderLogicaCartuchoALaMesa.SetActive(false);
    
        sliderLogicaOcultadorALaMesa.SetActive(true);
        ChangeAndShowConsejo("Coloca el ocultador en la mesa");
    }

    public void ColocarOcultadorAgujasEnMesa()
    {
        textoExplicacíon.text = textosDePasoEnOrden[7];
        TurnOffOutlines(estucheparaPasoDos.transform);
     
        sliderLogicaOcultadorALaMesa.SetActive(false);

        mesaPupitre.transform.parent = null;
        parentPasos[2].SetActive(false);
        parentPasos[3].SetActive(true);

        Invoke(nameof(PrepararLogicaSacarSurepalDeEstuche), 2);
    }

    public void PrepararLogicaSacarSurepalDeEstuche()
    {
        ChangeAndShowConsejo("Saca tu Surepal");

        sliderLogicaSacarSurepal.SetActive(true);
    }

    public void PrepararSurepalParaCapuchon()
    {

        sliderLogicaSacarSurepal.SetActive(false);
        SM_SurepalParaPaso3.SetActive(true);
        SM_SurepalEnAnimacionAborrarParaPaso3.SetActive(false);
        ChangeAndShowConsejo("Destapa tu Surepal");
  
        //sliderLogicaEnSurepalSacarTapon.SetActive(true);    
    }

    public void SacarCapuchonDeSurepal()
    {

        capuchonConDraggable.SetActive(false);
        capuchonParaVolverALaMesa.SetActive(true);
        SM_SurepalParaPaso3.GetComponent<Animator>().Play("RetiraSurepalParaVisibilidad");
        ChangeAndShowConsejo("Coloca el capuchón en el estuche");
        //sliderLogicaTaponAEstuche.SetActive(true);
        Invoke(nameof(InvokeAuxiliarColocarTapaEnEstuche),3);
    }

    public void InvokeAuxiliarColocarTapaEnEstuche() 
    {
        capuchonParaVolverALaMesa.GetComponent<Animator>().Play("RecolocarTapaSueltaEnEstuchePasoTres");
        Invoke(nameof(ColocartapaEnEstuche),6);
        Invoke(nameof(AuxInvokeRecolocaSurepal), 3);
    }

    public void AuxInvokeRecolocaSurepal() 
    {
        SM_SurepalParaPaso3.GetComponent<Animator>().Play("SurepalHighLightCierre");
        TurnOffOutlines(capuchonParaVolverALaMesa.transform);
    }

    public void ColocartapaEnEstuche()
    {
        textoExplicacíon.text = textosDePasoEnOrden[8];
        anilloDeCierrePaso3Sacar.SetActive(true);
        anilloDeCierrePaso3Visual.SetActive(false);
        sliderLogicaTaponAEstuche.SetActive(false);
        sliderLogicoDesenrroscaAnillo.SetActive(true);
        //SM_SurepalParaPaso3.GetComponent<Animator>().Play("SurepalHighLightCierre");
        TurnOnOutlines(SM_SurepalParaPaso3.transform);
        ChangeAndShowConsejo("Gira el anillo de cierre");
    }

    public void GirarAnilloDeCierre()
    {

        sliderLogicoDesenrroscaAnillo.SetActive(false);
     
        anilloDeCierrePaso3Sacar.GetComponent<BoxCollider>().enabled = true;
        anilloDeCierrePaso3Sacar.GetComponent<LinearDragable>().canInteract = true;
        ChangeAndShowConsejo("Extrae el anillo de cierre");
    }

    public void SacarAnilloDeCierreDeSurepal()
    {
     
        //sliderLogicoSueltaRoscaAnillo.SetActive(true);
        SM_SurepalParaPaso3FINAl.SetActive(true);
        //anilloDeCierreSurePalPaso3.gameObject.SetActive(false);
        SM_SurepalParaPaso3.SetActive(false);
        SM_SurepalParaPaso3FINAl.GetComponent<Animator>().Play("ColocarSurepalYCierreENMEsa");
        Invoke(nameof(MoverAnilloDeCierreALaMesa),5);
    }

    public void MoverAnilloDeCierreALaMesa()
    {
        sliderLogicoSueltaRoscaAnillo.SetActive(false);
        textoExplicacíon.text = textosDePasoEnOrden[9];
        ChangeAndShowConsejo("Identifica las partes del Surepal");
        Invoke(nameof(TurnOnEtiquetas), 2);
        Camera.main.gameObject.GetComponent<Animator>().Play("CamaraCloseUp");
    }

    public void CallClickAguja()
    {
        Invoke(nameof(ClickAguja), 1f);
        MoveToMousePositionPanelMuyBien();
        parentIndicadorAguja.gameObject.SetActive(false);
    }

    public void ClickAguja()
    {
        parentIndicadorCartucho.gameObject.SetActive(true);
    }

    public void CallClickCartucho()
    {
        Invoke(nameof(ClickCartucho), 1f);
        MoveToMousePositionPanelMuyBien();
        parentIndicadorCartucho.gameObject.SetActive(false);
    }

    public void ClickCartucho()
    {
        parentIndicadorOcultador.gameObject.SetActive(true);
    }

    public void CallClickOcultador()
    {
        Invoke(nameof(ClickOcultador), 1f);
        MoveToMousePositionPanelMuyBien();
        parentIndicadorOcultador.gameObject.SetActive(false);
    }
    public void ClickOcultador()
    {
        parentIndicadorAnilloDeCierre.gameObject.SetActive(true);
    }

    public void CallAnilloDeCierre()
    {
        Invoke(nameof(ClickAnilloDeCierre), 1f);
        MoveToMousePositionPanelMuyBien();
        parentIndicadorAnilloDeCierre.gameObject.SetActive(false);
    }
    public void ClickAnilloDeCierre()
    {
        parentIndicadorTapa.gameObject.SetActive(true);
    }
    public void CallClickTapa()
    {
        Invoke(nameof(ClickTapa), 1f);
        MoveToMousePositionPanelMuyBien();
        parentIndicadorTapa.gameObject.SetActive(false);
    }

    public void ClickTapa()
    {
        parentIndicadorSurepal.gameObject.SetActive(true);
    }

    public void CallClickSurepal()
    {
        Invoke(nameof(ClickSurepal), 1f);
        MoveToMousePositionPanelMuyBien();
        parentIndicadorSurepal.gameObject.SetActive(false);
    }

    public void ClickSurepal()
    {
        textoExplicacíon.text = textosDePasoEnOrden[10];
        parentPasos[4].gameObject.SetActive(true);
        Invoke(nameof(TraerContenedorAgujas), 1f);
    }

    public void TurnOnEtiquetas()
    {
        canvasEtiquetas.SetActive(true);
    }

    public void TraerContenedorAgujas()
    {
        disposalCan.GetComponent<Animator>().Play("ApareceDisposalDeAgujas");
        Invoke(nameof(ShowIndicadorAgujas), 3f);
        Invoke(nameof(CallClickContenedorAgujas), 6);
    }

    public void ShowIndicadorAgujas()
    {
        textoExplicacíon.text = textosDePasoEnOrden[10];
        parentIndicadorContenedorAgujas.gameObject.SetActive(true);
    }

    public void CallClickContenedorAgujas()
    {
        textoExplicacíon.text = textosDePasoEnOrden[11];
        Invoke(nameof(ClickContenedorAgujas), 1f);
        parentIndicadorContenedorAgujas.gameObject.SetActive(false);
        BajarToallitas();
    }

    public void ClickContenedorAgujas()
    {
        Invoke(nameof(EncenderToallitasBoton), 3);
    }

    public void EncenderToallitasBoton()
    {
        parentIndicadorToallitasAlcoholicas.gameObject.SetActive(true);
        Invoke(nameof(CallClickToallitas), 3);
    }

    public void CallClickToallitas()
    {
        Invoke(nameof(ClickToallitas), 1f);
        parentIndicadorToallitasAlcoholicas.gameObject.SetActive(false);
    }

    public void ClickToallitas()
    {
        parentIndicadorToallitasAlcoholicas.gameObject.SetActive(false);
        DarCambiazoPaso4();
        textoExplicacíon.text = textosDePasoEnOrden[12];
     
    }

    public void DarCambiazoPaso4()
    {
        SM_SurepalParaPaso3FINAl.SetActive(false);
        SM_SurepalEstuche.SetActive(false);
        piezasSueltasSurepalConEstuche.SetActive(true);
        parentPasos[5].SetActive(true);
        toallitasParent.transform.parent = parentPasos[5].transform.parent;
        disposalCan.transform.parent = parentPasos[5].transform.parent;
        parentPasos[4].SetActive(false);
        Invoke(nameof(EnciendeDraggablesParaAnilloDeCierrePaso4), 3);
    }

    public void BajarToallitas()
    {
        toallitasParent.GetComponent<Animator>().Play("AparecenToallitas");
    }

    #endregion

    #region PrepararSurepal

    public void EnciendeDraggablesParaAnilloDeCierrePaso4()
    {
        linearDraggableLogicoAnilloeCierre.SetActive(true);

    }

    public void ElevarAnilloDeCierreAlAirePaso4()
    {
        linearDraggableLogicoAnilloeCierre.SetActive(false);

        linearDraggableLogicaCartucho.gameObject.SetActive(true);
    
        MoveToMousePositionPanelMuyBien();
    }

    public void AlinearCartuchoConAnilloDeCierre()
    {
        linearDraggableLogicaCartucho.gameObject.SetActive(false);
 

        linearDraggableLogicaColocaCartucho.gameObject.SetActive(true);
    
        MoveToMousePositionPanelMuyBien();
    }

    public void AhoraTocaGirar()
    {
        linearDraggableLogicaColocaCartucho.gameObject.SetActive(false);


        Invoke(nameof(AuxiliarInvokeDraggableCartucho),4) ;
        MoveToMousePositionPanelMuyBien();
    }

    public void AuxiliarInvokeDraggableCartucho() 
    {
        linearDraggableLogicaGIRACartucho.SetActive(true);
    }

    public void CartuchoGirado()
    {
        linearDraggableLogicaGIRACartucho.SetActive(false);
   
        linearDraggableLogicaClickCartucho.SetActive(true);
   
        MoveToMousePositionPanelMuyBien();
    }

    public void CartuchoClick()
    {
        linearDraggableLogicaClickCartucho.SetActive(false);

        
        MoveToMousePositionPanelMuyBien();
        anilloDeCierrePaso6.GetComponent<Animator>().Play("AlinearAnilloDeCierreConSurepal");
        cartuchoEnAnilloDECierre.SetActive(true);
        cartuchoEnAnilloDEABorrar.SetActive(false);
        Invoke(nameof(EnableSlidersAlineaAnilloConSurepal), 3);
    }

    public void EnableSlidersAlineaAnilloConSurepal() 
    {
        linearDraggableLogicaAlineaSurepalYCierre.SetActive(true);
       
    }

    public void AlineaAnilloConSurepal() 
    {
        surepalPaso6.GetComponent<Animator>().Play("ColocarSurepalEnPosicionParaAnilloDeCierre");

        linearDraggableLogicaAlineaSurepalYCierre.SetActive(false);
        anilloDeCierrePaso6.GetComponent<Animator>().speed = 1;
        Invoke(nameof(PrepararEncajeCierreYSurepal), 4);
    }


    public void PrepararEncajeCierreYSurepal()
    {
        linearDraggableLogicaEncajarAnillo.SetActive(true);

    }

    public void DragAndDropAnilloDeCierreASurepal()
    {
        linearDraggableLogicaEncajarAnillo.SetActive(false);



        linearDraggableLogicaRotarAnilloEnSurepal.SetActive(true);
        MoveToMousePositionPanelMuyBien();
    }

    public void GiraAnilloDeCierreEnSurepal()
    {
     
        linearDraggableLogicaRotarAnilloEnSurepal.SetActive(false);
        SurepalPasoLimpiarPunta.SetActive(true);
        anilloDeCierrePaso6.SetActive(false);
        surepalPaso6.SetActive(false);
        parentPasos[6].SetActive(true);
        toallitasParent.GetComponent<Animator>().Play("ToallitaPreparadaParaLimpiar");
       
        linearDraggableLogicaLimpiarPuntaSurepal.SetActive(true);
    }

    public void LimpiarPuntaSurepalConToallita()
    {

        linearDraggableLogicaLimpiarPuntaSurepal.SetActive(false);
        toallitasParent.GetComponent<Animator>().Play("LimpiaSurepalAuto");
        toallitasParent.GetComponent<Animator>().speed = 1;
        //Invoke(nameof(LLamarAQueBajeLaPapelera), 6);
        Invoke(nameof(ColocaSurepalSobreLaMesaPostToallita), 7);
    }

    public void LLamarAQueBajeLaPapelera()
    {
        disposalCan.GetComponent<Animator>().Play("MoverDisposalParaWashcloth");
        washclothEspecialTirarALaBasura.SetActive(true);
   
        toallitasParent.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void TirarToallitaAPapelera()
    {

        washclothEspecialTirarALaBasura.SetActive(false);
        disposalCan.GetComponent<Animator>().Play("DisposalBack");
      
        Invoke(nameof(ColocaSurepalSobreLaMesaPostToallita), 2);
    }
    #endregion

    #region ComoPonermeLaInyeccion

    public void ColocaSurepalSobreLaMesaPostToallita()
    {
        SurepalPasoLimpiarPunta.GetComponent<Animator>().Play("SurepalDejarEnMesaPasoSiete");
        agujaAAbrir.GetComponent<Animator>().Play("AgujaEnPosicionParaDestapar");
        Invoke(nameof(PrepararDespegarPegatina), 3);
    }

    public void PrepararDespegarPegatina()
    {

        linearDraggableLogicaQuitarPegatina.SetActive(true);
        TurnOnOutlines(agujaAAbrir.transform);
    }

    public void DespegarPegatinaAgujaEmpaquetada()
    {

        linearDraggableLogicaQuitarPegatina.SetActive(false);
        MoveToMousePositionPanelMuyBien();

        Invoke(nameof(MoverSurepalYjeringuillaEnLinea), 0.5f);
    }

    public void MoverSurepalYjeringuillaEnLinea()
    {
        agujaAAbrir.GetComponent<Animator>().speed = 1;
        agujaAAbrir.GetComponent<Animator>().Play("ColocarJeringuillaEnLineaConSurepal");
        SurepalPasoLimpiarPunta.GetComponent<Animator>().Play("AlinearSurepalConAguja");
        Invoke(nameof(PrepararDraggablesAgujaYSurepal), 3);
    }

    public void PrepararDraggablesAgujaYSurepal()
    {
    
        linearDraggableLogicaAgujaEnSurepal.SetActive(true);
    }
    public void DragAndropAgujaAlSurepal()
    {
        linearDraggableLogicaAgujaEnSurepal.SetActive(false);
        linearDraggableLogicaRotarAgujaEnSurepal.SetActive(true);
    }

    public void GiraAgujaEmpaquetadaEnSurepal()
    {
        linearDraggableLogicaRotarAgujaEnSurepal.SetActive(false);
        Invoke(nameof(InvokeAuxiliarPostGiroAguja),2);
    }


    public void DragAndDropSacaEnvoltorioAgujaDeSurepal()
    {
        linearDraggableLogicaSacaCapuchaAgujaEnSurepal.SetActive(false);
     
        //agujaAAbrir.GetComponent<Animator>().Play("DejaCapuchonEnMesa");
        //agujaAAbrir.GetComponent<Animator>().speed = 1;
        //Cartel de que necesitará luego el capucho
        CubreAgujasSuelto.SetActive(true);
        CubreAgujasEnEstucheABorrar.SetActive(false);
        Invoke(nameof(EncenderDragsParaAlinearOcultadorAguja), 2);

    }
    public void InvokeAuxiliarPostGiroAguja()
    {
        linearDraggableLogicaSacaCapuchaAgujaEnSurepal.SetActive(true);
    }
    public void EncenderDragsParaAlinearOcultadorAguja() 
    {
        linearDragElevarCubreAgujas.SetActive(true);
        linearDraggableLogicaSacaCapuchaAgujaEnSurepal.SetActive(false);
        linearDraggableLogicaSacaCapuchaAgujaEnSurepal.SetActive(false);
    }

    public void PreparadoAlineadoOcultadorConSurepal() 
    {
        linearDragElevarCubreAgujas.SetActive(true);
        linearDraggableLogicaColocaOcultador.SetActive(false);
        TurnOnOutlines(CubreAgujasSuelto.transform);
    }

    public void AlineadoCorrectamenteOcultadorConSurepal() // ESTO ESTA LLAMANDO A QUE SE RETIRE LA AGUIJA 
    {
        linearDragElevarCubreAgujas.SetActive(false);
        linearDraggableLogicaColocaOcultador.SetActive(true);
        TurnOnOutlines(CubreAgujasSuelto.transform);
    }

    public void DragAndDropOcultadorDeagujaEnSurepal()
    {
        linearDraggableLogicaColocaOcultador.SetActive(false);
        Invoke(nameof(RetiraSurepalPorLaDerecha), 2);
        TurnOffOutlines(CubreAgujasSuelto.transform);
    }

    public void RetiraSurepalPorLaDerecha()
    {
        CubreAgujasSuelto.GetComponent<Animator>().Play("SacarCubreAgujasPorLaDerecha");
        CubreAgujasSuelto.GetComponent<Animator>().speed = 1;
        SurepalPasoLimpiarPunta.GetComponent<Animator>().Play("RetiraSurepalPorLaDerecha");
        agujaAAbrir.GetComponent<Animator>().Play("AgujaSeVaParaLaDerecha");
        capuchonABorrar.SetActive(false);
        capuchonSuelto.SetActive(true);
        Invoke(nameof(ApagarElementosAngtiguosSurepal), 2);
    }

    public void ApagarElementosAngtiguosSurepal()
    {
        CubreAgujasSuelto.SetActive(false);
        SurepalPasoLimpiarPunta.SetActive(false);
        agujaAAbrir.SetActive(false);
        parentPasos[7].SetActive(true);
        Invoke(nameof(PrepararBotonDosisGiro), 6);
    }

    public void PrepararBotonDosisGiro()
    {
        linearDraggableLogicaGirarDosis.SetActive(true);
    }

    public void GiraBotonDosisSurepal()
    {
        linearDraggableLogicaGirarDosis.SetActive(false);
        surepalDefinitivoFinal.Play("GirarDosisSurepal");
        Invoke(nameof(ElegirParteDelCuerpoParaPinchar), 8);
    }

    public void ElegirParteDelCuerpoParaPinchar()
    {
        parentPasos[8].SetActive(true);
    }

    public void TransicionACuerpo()
    {
        mesaPupitre.Play("PupitreSaleDePlano");
        canvasElegirAnimator.Play("SalirDePlano");
        Camera.main.GetComponent<Animator>().Play("CameraCloseUpBarriga");
        NinioParaMedicar.gameObject.SetActive(true);
        Invoke(nameof(CallAnimationNinioMedicarBarrigaArriba), 1.3f);
    }

    public void CallAnimationNinioMedicarBarrigaArriba()
    {
        NinioParaMedicar.GetComponent<Animator>().Play("NinioSeSubeCamiseta");
        toallitasParent.GetComponent<Animator>().Play("ToallitaSePoneCercaDeCuerpo");
        Invoke(nameof(PreparaLimpiadoDeToallas), 4);
    }


    #endregion


    #region PoniendomeLaInyeccion

    public void PreparaLimpiadoDeToallas()
    {

        linearDraggableLogicaLimpiaZona.SetActive(true);
    }


    public void DragAndDropToallitaAlcoholEnParteDelCuerpoBarriga()
    {
 
        linearDraggableLogicaLimpiaZona.SetActive(false);
        toallitasParent.GetComponent<Animator>().Play("ToallitaSaleDePlano");
        toallitasParent.GetComponent<Animator>().speed = 1;
        Invoke(nameof(MuestraSurepalConParteDelCuerpoBrazo), 2);
    }

    public void MuestraSurepalConParteDelCuerpoBrazo()
    {
        surepalDefinitivoFinal.GetComponent<Animator>().Play("SurePalFinalApareceCercaDeCuerpo");
        Invoke(nameof(PreparaEcharAtrasOcultadorAguja), 6);
    }

    public void PreparaEcharAtrasOcultadorAguja()
    {
      
        linearDraggableLogicaEcharSurepalHaciaAtras.SetActive(true);
    }

    public void EchaHaciaAtrasOcultadorDeAguja()
    {
 
        linearDraggableLogicaEcharSurepalHaciaAtras.SetActive(false);
     
        linearDraggableLogicaSacarProtectorDeAguja.SetActive(true);
    }


    public void PrepararSurepalParaPinchar()
    {

        linearDraggableLogicaSacarProtectorDeAguja.SetActive(false);
        surepalDefinitivoFinal.Play("PrepararSurepalParaPinchar");
        surepalDefinitivoFinal.speed = 1;
        Invoke(nameof(SacarDragAndDropParaPinchar), 4f);
    }

    public void SacarDragAndDropParaPinchar()
    {
  
        linearDraggableLogicaPincharSurepal.SetActive(true);
    }

    public void PincharBarriga()
    {
  
        linearDraggableLogicaPincharSurepal.SetActive(false);
    
        linearDraggableLogicaPulsarBotonEnSurepal.SetActive(true);

        linearDraggableLogicaSacarSurepal.SetActive(true);
    }

    public void PresionarBoton() 
    {
        linearDraggableLogicaPulsarBotonEnSurepal.SetActive(false);
        TurnOnOutlines(botonSurepal.transform);
       
        linearDraggableLogicaSacarSurepal.SetActive(true);
    }

    public void RetirarSurepal()
    {
        linearDraggableLogicaSacarSurepal.SetActive(false);
      
        surepalDefinitivoFinal.GetComponent<Animator>().Play("RetirarSurepal");
        surepalDefinitivoFinal.GetComponent<Animator>().speed = 1;
    }

    public void TimerDone()
    {
        linearDraggableLogicaPulsarBotonEnSurepal.GetComponentInChildren<LinearDragable>().canInteract = false;
        TurnOffOutlines(surepalDefinitivoFinal.transform.GetChild(3));
        TurnOffOutlines(botonSurepal.transform);
    }


    public void SelecionarAnguloPinchado()
    {

    }

    public void DragAndDropPinchaEnElAnguloSeleccionado()
    {

    }

    public void PulsaElBotonSurepalMientrasPincha()
    {

    }

    public void IniciaCuentaAtrasBotonSurepal()
    {

    }

    public void InterrumpeCuentaAtrasBotonSurepal()
    {

    }

    public void TerminaConExitoCuentaAtrasSurepal()
    {

    }

    public void DragAndDropSacarSurepalDelBrazo()
    {

    }

    #endregion

    #region PostInyeccion
    public void StartPostInyeccion()
    {

    }

    public void ComprobarDosisPostInyeccion()
    {

    }

    public void MostrarSurepalDeLadoConProtector()
    {

    }

    public void DragAndDropSacarOcultadorDeAguja()
    {

    }

    public void DragAndDropGuardarOcultadorDeAgujaEnEstuche()
    {

    }

    public void MostrarProtectorDeAgujaAlineadoConSurepal()
    {

    }

    public void DragAndDropColocarProtectorDeAguja()
    {

    }

    public void GirarProtectorDeAgujaParaDescolocar()
    {

    }

    public void DragAndDropProtectorDeAgujaEnContenedor()
    {

    }

    public void MostrarSurepalSinAgujaConCierreListoParaGirar()
    {

    }

    public void DesenroscarAnilloDeCierre()
    {

    }

    public void DragAndDropSacarAnilloDeCierre()
    {

    }

    public void DragAndDropSacarCartuchoDeAnilloDeCierre()
    {

    }

    public void DragAndDropTiraCartuchoVacioAPapelera()
    {

    }

    public void RencajaAnilloDeCierre()
    {

    }

    public void GiraAnilloDeCierreSinAguja()
    {

    }

    public void DragAndDropSurepalAlEstuche()
    {

    }

    public void DragAndDropCerrarEstuche()
    {

    }

    public void ApareceNeveraYSeAbre()
    {

    }

    public void DragAndDropGuardaSurepalEnNevera()
    {

    }


    #endregion

}

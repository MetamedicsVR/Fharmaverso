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

    public Animator [] surepalErroneosARetirar;

    public GameObject panelMuyBien;

    public GameObject [] parentPasos;
    [Header("Primer Paso")]
    public Animator neveraAnimator;
    public Animator pomoNevera;
    public GameObject dragAndDropAbrirNeveraVisual;
    public GameObject dragAndDropAbrirNeveraLogica;

    public GameObject dragAndDropSacarSurepalVisual;
    public GameObject dragAndDropSacarSurepalLogica;

    public GameObject surepalEnNevera;

    public GameObject surepalParaAnimacionFueraDeNevera;
    [Header("Segundo Paso")]

    public GameObject estucheparaPasoDos;
    public Animator mesaPupitre;
    public GameObject cartuchoEnEstucheEnMesa;

    public GameObject sliderVisualSacarAguja;
    public GameObject sliderLogicaSacarAguja;

    public GameObject sliderVisualAgujaEnMesa;
    public GameObject sliderLogicaAgujaEnMesa;

    public GameObject sliderVisualCartuchoALaMesa;
    public GameObject sliderLogicaCartuchoALaMesa;

    public GameObject sliderVisualOcultadorALaMesa;
    public GameObject sliderLogicaOcultadorALaMesa;

    [Header("Tercer Paso")]
    public GameObject SM_SurepalParaPaso3;
    public GameObject SM_SurepalEnAnimacionAborrarParaPaso3;
    public GameObject SM_SurepalEstuche;
    public GameObject sliderVisualSacarSurepal;
    public GameObject sliderLogicaSacarSurepal;
    public GameObject sliderVisualSacarTaponSurepal;
    public GameObject sliderLogicaEnSurepalSacarTapon;


    public GameObject capuchonConDraggable;

    public GameObject capuchonParaVolverALaMesa;

    public GameObject sliderVisualTaponAEstuche;
    public GameObject sliderLogicaTaponAEstuche;

    public Transform anilloDeCierreSurePalPaso3;


    public GameObject sliderVisualDesenrroscaAnillo;

    public GameObject sliderLogicoDesenrroscaAnillo;

   
    public GameObject anilloDeCierrePaso3Sacar;

    public GameObject sliderVisualSacaAnilloDeCierrePaso3;

    public GameObject SM_SurepalParaPaso3FINAl;

    public GameObject sliderVisuaSueltaRoscaAnillo;

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
    public GameObject linearDraggableVisualCartucho;

    public GameObject linearDraggableLogicoAnilloeCierre;
    public GameObject linearDraggableVisualAnilloeCierre;


    public GameObject linearDraggableLogicaColocaCartucho;
    public GameObject linearDraggableVisualColocaCartucho;

    public GameObject linearDraggableLogicaGIRACartucho;
    public GameObject linearDraggableVisualGIRACartucho;

    public GameObject linearDraggableLogicaClickCartucho;
    public GameObject linearDraggableVisualClickCartucho;
    [Header("SextoPaso")]
    public GameObject surepalPaso6;
    public GameObject anilloDeCierrePaso6;
    public GameObject cartuchoEnAnilloDECierre;
    public GameObject cartuchoEnAnilloDEABorrar;

    public GameObject linearDraggableLogicaEncajarAnillo;
    public GameObject linearDraggableVisualEncajarAnillo;


    public GameObject linearDraggableVisualRotarAnilloEnSurepal;
    public GameObject linearDraggableLogicaRotarAnilloEnSurepal;

    #region Auxiliares

    public void ChangeAndShowConsejo(string consejoTexto) 
    {
        panelConsejo.GetComponentInChildren<TextMeshProUGUI>().text = consejoTexto;
        panelConsejo.Play("MostrarConsejo");

    }
    public void TurnOffOutlines(Transform parentGameobject) 
    {
        Outline outline = parentGameobject.GetComponent<Outline>();
        if(outline)
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
        Invoke(nameof(AparecerNevera),10);
    }

    public void AparecerNevera() 
    {
        parentPasos[0].SetActive(false);
        parentPasos[1].SetActive(true);
        Invoke(nameof(EnableSliderVisualNevera), 3);
    }

    public void EnableSliderVisualNevera() 
    {
        dragAndDropAbrirNeveraVisual.SetActive(true);
        ChangeAndShowConsejo("Selecciona el asa y arrastra");
    }

    public void BajarPomoNevera() 
    {
        pomoNevera.CrossFade("BajaPomoNevera",0.5f);
    }

    public void SubirPomoNevera()
    {
        pomoNevera.CrossFade("SubePomoNevera", 0.5f);
    }

    public void SlideNeveraTerminado() 
    {
        TurnOffOutlines(neveraAnimator.transform);
        MoveToMousePositionPanelMuyBien();
        dragAndDropAbrirNeveraVisual.SetActive(false);
        dragAndDropAbrirNeveraLogica.SetActive(false);
        neveraAnimator.Play("NeveraCloseUp");
        Invoke(nameof(EnableSliderVisualSacarSurepalDeNevera), 1);
    }

    public void EnableSliderVisualSacarSurepalDeNevera()
    {
        dragAndDropSacarSurepalVisual.SetActive(true);
        dragAndDropSacarSurepalLogica.SetActive(true);
        ChangeAndShowConsejo("Selecciona el estuche y arrastra");
        TurnOnOutlines(surepalEnNevera.transform);
    }

    public void SacarDispositivoDeNevera() 
    {
        dragAndDropSacarSurepalVisual.SetActive(false);
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
        sliderVisualSacarAguja.SetActive(true);
        sliderLogicaSacarAguja.SetActive(true);
        ChangeAndShowConsejo("Saca una aguja del estuche");
    }

    public void AgujasEnElAire() 
    {
        textoExplicacíon.text = textosDePasoEnOrden[4];
        TurnOffOutlines(estucheparaPasoDos.transform);
        sliderVisualAgujaEnMesa.SetActive(true);
        sliderLogicaAgujaEnMesa.SetActive(true);
        sliderVisualSacarAguja.SetActive(false);
        sliderLogicaSacarAguja.SetActive(false);
        ChangeAndShowConsejo("Coloca la aguja en la mesa");

    }
    public void ColocarAgujasEnMesa()
    {
        textoExplicacíon.text = textosDePasoEnOrden[5];
        TurnOnOutlines(cartuchoEnEstucheEnMesa.transform);
        sliderVisualAgujaEnMesa.SetActive(false);
        sliderLogicaAgujaEnMesa.SetActive(false);
        sliderVisualCartuchoALaMesa.SetActive(true);
        sliderLogicaCartuchoALaMesa.SetActive(true);
        ChangeAndShowConsejo("Coloca el cartucho sobre la mesa");
        //enciende los nuevos sliders 
    }

    public void ColocarCartuchoEnMesa() 
    {
        TurnOffOutlines(cartuchoEnEstucheEnMesa.transform);
        cartuchoEnEstucheEnMesa.GetComponent<Outline>().enabled = false;
        textoExplicacíon.text = textosDePasoEnOrden[6];
        sliderVisualCartuchoALaMesa.SetActive(false);
        sliderLogicaCartuchoALaMesa.SetActive(false);
        sliderVisualOcultadorALaMesa.SetActive(true); 
        sliderLogicaOcultadorALaMesa.SetActive(true);
        ChangeAndShowConsejo("Coloca el ocultador en la mesa");
     
    }

    public void ColocarOcultadorAgujasEnMesa() 
    {
        textoExplicacíon.text = textosDePasoEnOrden[7];
        TurnOffOutlines(estucheparaPasoDos.transform);
        sliderVisualOcultadorALaMesa.SetActive(false);
        sliderLogicaOcultadorALaMesa.SetActive(false);
     
        mesaPupitre.transform.parent = null;
        parentPasos[2].SetActive(false);
        parentPasos[3].SetActive(true);

        Invoke(nameof(PrepararLogicaSacarSurepalDeEstuche),2);
    }

    public void PrepararLogicaSacarSurepalDeEstuche() 
    {
        ChangeAndShowConsejo("Saca tu Surepal");
        sliderVisualSacarSurepal.SetActive(true);
        sliderLogicaSacarSurepal.SetActive(true);
    }

    public void PrepararSurepalParaCapuchon() 
    {
        sliderVisualSacarSurepal.SetActive(false);
        sliderLogicaSacarSurepal.SetActive(false);
        SM_SurepalParaPaso3.SetActive(true);
        SM_SurepalEnAnimacionAborrarParaPaso3.SetActive(false);
        ChangeAndShowConsejo("Destapa tu Surepal");
        sliderVisualSacarTaponSurepal.SetActive(true);
        //sliderLogicaEnSurepalSacarTapon.SetActive(true);    
    }

    public void SacarCapuchonDeSurepal()
    {
        sliderVisualSacarTaponSurepal.SetActive(false);
        capuchonConDraggable.SetActive(false);
        capuchonParaVolverALaMesa.SetActive(true);
        SM_SurepalParaPaso3.GetComponent<Animator>().Play("RetiraSurepalParaVisibilidad");
        ChangeAndShowConsejo("Coloca el capuchón en el estuche");
        sliderLogicaTaponAEstuche.SetActive(true);
        sliderVisualTaponAEstuche.SetActive(true);


    }

    public void ColocartapaEnEstuche() 
    {
        textoExplicacíon.text = textosDePasoEnOrden[8];
        sliderLogicaTaponAEstuche.SetActive(false);
        sliderVisualTaponAEstuche.SetActive(false);
        sliderVisualDesenrroscaAnillo.SetActive(true);
        sliderLogicoDesenrroscaAnillo.SetActive(true);
        SM_SurepalParaPaso3.GetComponent<Animator>().Play("SurepalHighLightCierre");
        TurnOnOutlines(SM_SurepalParaPaso3.transform);
        ChangeAndShowConsejo("Gira el anillo de cierre");

    }

    public void GirarAnilloDeCierre() 
    {
        sliderVisualDesenrroscaAnillo.SetActive(false);
        sliderLogicoDesenrroscaAnillo.SetActive(false);
        sliderVisualSacaAnilloDeCierrePaso3.SetActive(true);
        anilloDeCierrePaso3Sacar.GetComponent<BoxCollider>().enabled = true;
        anilloDeCierrePaso3Sacar.GetComponent<LinearDragable>().canInteract = true;
        ChangeAndShowConsejo("Extrae el anillo de cierre");
    }

    public void SacarAnilloDeCierreDeSurepal() 
    {
        sliderVisualSacaAnilloDeCierrePaso3.SetActive(false);
        sliderVisuaSueltaRoscaAnillo.SetActive(true);
        sliderLogicoSueltaRoscaAnillo.SetActive(true);
        SM_SurepalParaPaso3FINAl.SetActive(true);
        //anilloDeCierreSurePalPaso3.gameObject.SetActive(false);
        SM_SurepalParaPaso3.SetActive(false);
    }

    public void MoverAnilloDeCierreALaMesa() 
    {
        sliderVisuaSueltaRoscaAnillo.SetActive(false);
        sliderLogicoSueltaRoscaAnillo.SetActive(false);
        textoExplicacíon.text = textosDePasoEnOrden[9];
        ChangeAndShowConsejo("Identifica las partes del Surepal");
        Invoke(nameof(TurnOnEtiquetas), 2);
        Camera.main.gameObject.GetComponent<Animator>().Play("CamaraCloseUp");
    }


    public void CallClickAguja()
    {
        Invoke(nameof(ClickAguja),1f);
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
        Invoke(nameof(ShowIndicadorAgujas),3f);
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
        MoveToMousePositionPanelMuyBien();
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
    }
    public void CallClickToallitas()
    {
        Invoke(nameof(ClickToallitas), 1f);
        MoveToMousePositionPanelMuyBien();
        parentIndicadorToallitasAlcoholicas.gameObject.SetActive(false);
    
    }

    public void ClickToallitas()
    {
        parentIndicadorToallitasAlcoholicas.gameObject.SetActive(false);
        DarCambiazoPaso4();
        textoExplicacíon.text = textosDePasoEnOrden[12];
        MoveToMousePositionPanelMuyBien();
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
        linearDraggableVisualAnilloeCierre.SetActive(true);
    }
    public void ElevarAnilloDeCierreAlAirePaso4()
    {
        linearDraggableLogicoAnilloeCierre.SetActive(false);
        linearDraggableVisualAnilloeCierre.SetActive(false);
        linearDraggableLogicaCartucho.gameObject.SetActive(true);
        linearDraggableVisualCartucho.gameObject.SetActive(true);
        MoveToMousePositionPanelMuyBien();
    }

    public void AlinearCartuchoConAnilloDeCierre()
    {
        linearDraggableLogicaCartucho.gameObject.SetActive(false);
        linearDraggableVisualCartucho.gameObject.SetActive(false);

        linearDraggableLogicaColocaCartucho.gameObject.SetActive(true);
        linearDraggableVisualColocaCartucho.gameObject.SetActive(true);
        MoveToMousePositionPanelMuyBien();
    }

    public void AhoraTocaGirar() 
    {
        linearDraggableLogicaColocaCartucho.gameObject.SetActive(false);
        linearDraggableVisualColocaCartucho.gameObject.SetActive(false);
        linearDraggableLogicaGIRACartucho.SetActive(true);
        linearDraggableVisualGIRACartucho.SetActive(true);
        MoveToMousePositionPanelMuyBien();
    }

    public void CartuchoGirado() 
    {

        linearDraggableLogicaGIRACartucho.SetActive(false);
        linearDraggableVisualGIRACartucho.SetActive(false);
        linearDraggableLogicaClickCartucho.SetActive(true);
        linearDraggableVisualClickCartucho.SetActive(true);
        MoveToMousePositionPanelMuyBien();
    }

    public void CartuchoClick()
    {
        print("CartuchoClicado");
        linearDraggableLogicaClickCartucho.SetActive(false);
        linearDraggableVisualClickCartucho.SetActive(false);
        surepalPaso6.GetComponent<Animator>().Play("ColocarSurepalEnPosicionParaAnilloDeCierre");
        anilloDeCierrePaso6.GetComponent<Animator>().Play("AlinearAnilloDeCierreConSurepal");
        anilloDeCierrePaso6.GetComponent<Animator>().speed = 1;
        MoveToMousePositionPanelMuyBien();
        cartuchoEnAnilloDECierre.SetActive(true);
        cartuchoEnAnilloDEABorrar.SetActive(false);
        Invoke(nameof(PrepararEncajeCierreYSurepal),2);
    }


    public void PrepararEncajeCierreYSurepal() 
    {
      
        linearDraggableLogicaEncajarAnillo.SetActive(true);
        linearDraggableVisualEncajarAnillo.SetActive(true);
    }
    public void DragAndDropAnilloDeCierreASurepal()
    {
        linearDraggableLogicaEncajarAnillo.SetActive(false);
        linearDraggableVisualEncajarAnillo.SetActive(false);

        linearDraggableVisualRotarAnilloEnSurepal.SetActive(true);
        linearDraggableLogicaRotarAnilloEnSurepal.SetActive(true);
        MoveToMousePositionPanelMuyBien();
    }

    public void GiraAnilloDeCierreEnSurepal()
    {
        linearDraggableVisualRotarAnilloEnSurepal.SetActive(false);
        linearDraggableLogicaRotarAnilloEnSurepal.SetActive(false);
    }

    #endregion

    #region ComoPonermeLaInyeccion

    public void StartComoPonermeLaInyeccion() 
    {
    
    }

    public void MostrarSurepalAlineadoConToallita() 
    {
    
    }

    public void DragAndDropToallitaEnSurepal() 
    {
    
    }

    public void ColocaSurepalSobreLaMesaPostToallita() 
    {
    
    }

    public void MostrarAgujaEmpaquetada() 
    {
    
    }

    public void DespegarPegatinaAgujaEmpaquetada() 
    {
    
    }

    public void MostrarSurepalAlineadoConAgujaAbierta() 
    {
    
    }

    public void DragAndropAgujaAlSurepal() 
    {
    
    }

    public void GiraAgujaEmpaquetadaEnSurepal() 
    {
    
    }

    public void DragAndDropSacaEnvoltorioAgujaDeSurepal() 
    {
    
    }

    public void DejarEnvoltorioDeAgujaEnLaMesa() 
    {
    
    }

    public void MostrarOcultadorDeAgujasAlineadoConSurepal() 
    {
    
    }

    public void DragAndDropOcultadorDeagujaEnSurepal() 
    {
    
    }

    public void GiraBotonDosisSurepal() 
    {
    
    }

    public void ElegirParteDelCuerpoParaPinchar() 
    {
    
    }



    #endregion


    #region PoniendomeLaInyeccion

    public void StartPoninedomeLaInyeccion() 
    {
    
    }

    public void MostrarToallitaConAlcohol() 
    {
    
    }

    public void DragAndDropToallitaAlcoholEnParteDelCuerpoBrazo() 
    {
    
    }

    public void MuestraSurepalConParteDelCuerpoBrazo()
    {

    }

    public void EchaHaciaAtrasOcultadorDeAguja() 
    {
    
    }

    public void DragAndDropSacarProtectorDeAguja() 
    {
    
    }

    public void MostrarSetupPinchadoBrazo() 
    {
    
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

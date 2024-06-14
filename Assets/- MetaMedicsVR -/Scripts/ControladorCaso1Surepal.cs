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

    public GameObject dragAndDropAbrirNeveraVisual;
    public GameObject dragAndDropAbrirNeveraLogica;

    public GameObject dragAndDropSacarSurepalVisual;
    public GameObject dragAndDropSacarSurepalLogica;

    public GameObject surepalEnNevera;

    public GameObject surepalParaAnimacionFueraDeNevera;
    [Header("Segundo Paso")]

    public GameObject estucheparaPasoDos;
    public Animator mesaPupitre;

    public GameObject sliderVisualSacarAguja;
    public GameObject sliderLogicaSacarAguja;


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
        Invoke(nameof(DesactivarPanelMuyBien), 1.5f);
    }

    public void DesactivarPanelMuyBien() 
    {
        panelMuyBien.SetActive(false);
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
        surepalParaAnimacionFueraDeNevera.transform.parent = parentPasos[2].transform;
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
    public void ColocarAgujasEnMesa()
    {
        
    }
    public void SacarOcultadorAgujas() 
    {
    
    }
    public void ColocarAgujasEnMesaOcultadorAgujasEnMesa()
    {

    }

    public void SacarCartuchoDeHormona()
    {

    }

    public void ColocarCartuchoDeHormonaEnMesa()
    {

    }

    public void SacaSurepal()
    {

    }

    public void QuitaTapaASurepal()
    {

    }

    public void ColocarTapaASurepalEnEstuche()
    {

    }

    public void DesenroscarAnilloDeCierreSurepal()
    {

    }

    public void ColocarAnilloDeCierreSurepalEnMesa()
    {

    }

    public void ColocarSurepalEnMesa()
    {

    }

    public void DisplayNombreDeTodasLasPartes() 
    {
    
    }

    public void MostrarContenedorAgujas()
    {

    }
    public void MostrarToallitasAlcoholicas()
    {

    }

    #endregion

    #region PrepararSurepal


    public void StartPrepararSurepal()
    {

    }

    public void AparecenAnilloDeCierreConCartuchoAlineados()
    {

    }

    public void DragAndDropCartuchoACierre()
    {

    }

    public void GirarCartuchoColocadoALaIzquierda()
    {

    }

    public void TirarHaciaAbajoDeCartuchoColocado()
    {

    }

    public void AparecenSurepalYAnilloDeCierreAlineados()
    {

    }

    public void DragAndDropEncajaAnilloDeCierreConSurepal()
    {

    }

    public void GiraAnilloDeCierreEnSurepal()
    {

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

using UnityEngine;

namespace Core.Example
{
    public class PDFExample : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialPDF;

        void Start()
        {
            MessageCenter.AddListener(PDFNote.Activate, ShowPDF);
            MessageCenter.AddListener(PDFNote.Hide, HidePDF);
        }

        // Send Msg call/trigger
        void ShowPDF(bool Activate)
        {
            // Show PDF
            tutorialPDF.SetActive(Activate);
            // Example
            // TODO : Animate In out on Activate
            // TODO : generate content
        }

        void HidePDF() 
        {
            // Hide
            tutorialPDF.SetActive(false);
            // Despose data
        }

        private void OnDestroy()
        {
            // Dispose Listener
            MessageCenter.RemoveListener(PDFNote.Activate, ShowPDF);
            MessageCenter.RemoveListener(PDFNote.Hide);
        }
    }
}
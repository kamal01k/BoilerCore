using Core;
using Core.Example;
using UnityEngine;

public class PDFTriggerExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MessageCenter.Send(PDFNote.Activate, true);
    }

    void HidePDF()
    {
        MessageCenter.Send(PDFNote.Hide);
    }
}

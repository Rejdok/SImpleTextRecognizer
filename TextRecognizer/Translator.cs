using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Translation.V2;

namespace TextTranslator
{
    public class Translator
    {
        public List<string> TranslateText(List<string> textToTranslate)
        {
            return (List<string>)trcl.TranslateText(textToTranslate, "en", "ru");
        }
        TranslationClient trcl = TranslationClient.Create(model:TranslationModel.NeuralMachineTranslation);
    }
}

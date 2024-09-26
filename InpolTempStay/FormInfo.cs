using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InpolTempStay
{
    internal class FormInfo
    {
        public string KodWnioskuInpol { get; set; }
        public string KodWnioskuMOS { get; set; }
        public string ImieCudzoziemca { get; set; }
        public string NazwiskoCudzoziemca { get; set; }
        public string DataUrodzenia { get; set; }
        public string NumerPaszportu { get; set; }
        public string Obywatelstwo { get; set; }
        public string NumerTelefonuRP { get; set; }
        public string EmailPowiadomienie { get; set; }
        public string PobytNaPodstawie { get; set; } /// wyszukanie po tekscie
        public string DataWaznosciDokumentu{ get; set; }
        public string DanePelnomocnika { get; set; }
    }
}

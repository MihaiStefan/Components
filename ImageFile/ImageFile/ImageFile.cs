using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageFile
{
    public class ImageObj
    {

    }

    public class ImageFile:Object
    {
        #region Public interfaces
        public string FileName
        {
            get
            {
                return this._FileName;
            }
            set
            {
                this._FileName = value;
            }
        }
        #endregion

        #region Private variables
        private string _FileName;
        #endregion

        #region Public Methods

        public ImageFile(string value)
        {
            this._FileName = value;

        }
        #endregion

        #region Private Methods
        ~ImageFile()
        {
            System.Windows.Forms.MessageBox.Show("distrus");
            //de salvat si inchis fisierul
            //se creeaza fisierul la constructor
            //se creaza un alt obiect serializabil, care va avea o lista in obiectul asta si care va putea fi salvata
            //+ sa vad cum se mostenesc automat constructorii
            //++ de vazut cum se poate face multitask incarcarea pozelor in view
            //++ daca tot se face incarcare multitask, sa fie posibila intreruperea si ulterior reluarea incarcarii pozelor din view
            //++ view sa poate sa vizualizeze poze de pe HDD, net sau baza de date.
        }
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWinphone2017.ViewModels
    {
    public class NoteModel : INotifyPropertyChanged
        {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
            {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
                {
                handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }

        private string _id;

        public string ID
            {
            get { return _id; }
            set
                {
                if (value != _id)
                    {
                    _id = value;
                    NotifyPropertyChanged("ID");
                    }
                }
            }

        private string _modDate;

        public string ModifiedDate
            {
            get { return _modDate; }
            set
                {
                if (value != _modDate)
                    {
                    _modDate = value;
                    NotifyPropertyChanged("ModifiedDate");
                    }
                }
            }


        private string _note;

        public string Note
            {
            get { return _note; }
            set
                {
                if (value != _note)
                    {
                    _note = value;

                    string[] lines = _note.Split('\n');

                    if (lines.Length > 0)
                        {
                        FirstLine = lines[0];
                        }
                    else
                        FirstLine = "(empty)";
                    NotifyPropertyChanged("Note");
                    }
                }
            }

        private string _firstLine;

        public string FirstLine
            {
            get { return _firstLine; }
            set
                {
                if (value != _firstLine)
                    {
                    _firstLine = value;
                    NotifyPropertyChanged("FirstLine");
                    }
                }
            }

        }
    }

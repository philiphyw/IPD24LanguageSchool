using System;
using System.Data.Entity;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ToDoMVVM.Library
{
    public class ToDoDBContext : DbContext
    {
        public ToDoDBContext() : base("name=ToDoDBContext") { }

        public virtual DbSet<ToDo> Todo { get; set; }
    }


    public enum STATUS { Pending = 0, Done = 1, Delegated = 2 }

    //public class ToDoModel { }

    //[Table("ToDo")]
    public class ToDo : INotifyPropertyChanged
    {
        private string _task;
        private int _difficulty;
        private DateTime _dueDate;
        private STATUS _status;
        private string dateFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public ToDo() { }

        public ToDo(ToDo item) 
        {
            ID = item.ID;
            Task = item.Task;
            Difficulty = item.Difficulty;
            DueDate = item.DueDate;
            Status = item.Status;
        }

        public ToDo(string task, int dif, DateTime due, STATUS todostatus)
        {
            Task = task;
            Difficulty = dif;
            DueDate = due;
            Status = todostatus;
        }

        public ToDo(string dataLine)
        {
            string[] inputs = dataLine.Split(';');
            if (inputs == null || inputs.Length != 4)
            {
                throw new InvalidParameterException($"Incorrect format of data line {dataLine}");
            }
            else
            {
                Task = inputs[0];
                int diff;
                if (int.TryParse(inputs[1], out diff))
                {
                    Difficulty = diff;
                }
                else
                {
                    throw new InvalidParameterException($"Invalid Difficulty  {inputs[1]}");
                }
                DateTime due;
                if (Util.parseToDateTime(inputs[2], dateFormat, out due))
                {
                    DueDate = due;
                }
                else
                {
                    throw new InvalidParameterException($"Invalid Due Date  {inputs[2]}");
                }
                STATUS st;
                if (Enum.TryParse(inputs[3], out st))
                {
                    Status = st;
                }
                else
                {
                    throw new InvalidParameterException($"Invalid Status {inputs[3]}");
                }
            }
        }

        public override string ToString()
        {
            return $"{Task};{Difficulty};{DueDate:d};{Status}";
        }

        [Key]
        public int ID
        {
            get;
            set;
        }

        [Required] // NOT NULL
        [StringLength(50)] // NVARCHAR(50)
        public string Task
        {
            get
            {
                return _task;
            }
            set
            {
                Regex pattern = new Regex(@"^[A-Za-z0-9 %_\-\(\),\.\/\\]{1,50}$");
                if (!pattern.IsMatch(value) || String.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidParameterException($"Invalid input of Task {value}");
                }
                else
                {
                    if (_task != value)
                    {
                        _task = value;
                        RaisePropertyChanged("Task");
                    }
                }
            }
        }

        [Required] // NOT NULL
        public int Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                if (value < 1 || value > 5)
                {
                    throw new InvalidParameterException($"Invalid input of Difficulty {value}");
                }
                else
                {
                    if (_difficulty != value)
                    {
                        _difficulty = value;
                        RaisePropertyChanged("Difficulty");
                    }
                }
            }
        }

        [NotMapped]
        public string DueDateCurrCulture => $"{_dueDate:d}";

        [Required] // NOT NULL
        public DateTime DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                if (value.Year < 1900 || value.Year > 2100)
                {
                    throw new InvalidParameterException($"Invalid input of Due Date {value}");
                }
                else
                {
                    if (_dueDate != value)
                    {
                        _dueDate = value;
                        RaisePropertyChanged("DueDate");
                    }
                }
            }
        }

        [Required] // NOT NULL
        [EnumDataType(typeof(STATUS))]
        public STATUS Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (Enum.IsDefined(typeof(STATUS), value))
                {
                    if (_status != value)
                    {
                        _status = value;
                        RaisePropertyChanged("Status");
                    }
                }
                else
                {
                    throw new InvalidParameterException($"Invalid input of Status {value}");
                }
            }
        }
    }

}
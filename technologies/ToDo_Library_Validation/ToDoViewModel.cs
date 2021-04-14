using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ToDoMVVM.Library
{
    public class ToDoViewModel :  IDataErrorInfo  //validation
    {
        private ToDoDBContext database;
        public ObservableCollection<ToDo> ToDos { get; set; }
        public ToDoCommand DeleteCommand { get;  set; }
        public ToDoCommand UpdateCommand { get; set; }
        public ToDoCommand AddCommand { get; set; }

        public ToDoViewModel()
        {
            Log.setLogOnFile();
            try
            {
                database = new ToDoDBContext();
            }
            catch (SystemException ex)
            {
                Log.WriteLine(ex.Message);
                Environment.Exit(1);
            }
            ToDos = new ObservableCollection<ToDo>();
            LoadToDos();
            DeleteCommand = new ToDoCommand(this.OnDelete, this.CanExecute);
            UpdateCommand = new ToDoCommand(this.OnUpdate, this.CanExecute);
            AddCommand = new ToDoCommand(this.OnAdd, this.CanAdd);

        }

        private void LoadToDos()
        {
            try
            {

                var todoList = database.Todo.ToList();
                ToDos.Clear();
                foreach(var item in todoList)
                {
                    ToDos.Add(item);
                }
            }
            catch (SystemException ex)
            {
                Log.WriteLine(ex.Message);
            }
        }

        private ToDo _selectedTodo;
        public ToDo SelectedTodo
        {
            get
            {
                return _selectedTodo;
            }
            set
            {
                _selectedTodo = value;
            }
        }

        public void OnDelete()
        {
            database.Todo.Remove(SelectedTodo);
            database.SaveChanges();
            LoadToDos();
        }
        public bool CanExecute()
        {
            if(SelectedTodo != null){
                return true;
            }
            return false;
        }
        public void OnUpdate()
        {
            try
            {
                var item = (from td in database.Todo where td.ID == SelectedTodo.ID select td).FirstOrDefault<ToDo>();
                if (item != null)
                {
                    item.Task = SelectedTodo.Task;
                    item.Difficulty = SelectedTodo.Difficulty;
                    item.DueDate = SelectedTodo.DueDate;
                    item.Status = SelectedTodo.Status;
                }
                database.SaveChanges();
                LoadToDos();
            }
            catch (Exception ex)
                when ((ex is InvalidParameterException) || (ex is SystemException))
            {
                Log.WriteLine(ex.Message);
            }
        }
        private ToDo _newTodo = new ToDo();
        //validation
        private string[] _errorMessage = new string[4] { string.Empty, string.Empty, string.Empty, string.Empty };
        public string Error
        {
            get { return null; }
        }
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Task":
                        if (!String.IsNullOrEmpty(_errorMessage[0]))
                            return _errorMessage[0];
                        break;
                    case "Difficulty":
                        if (!String.IsNullOrEmpty(_errorMessage[1]))
                            return _errorMessage[1];
                        break;
                    case "DueDate":
                        if (!String.IsNullOrEmpty(_errorMessage[2]))
                            return _errorMessage[2];
                        break;
                    case "Status":
                        if (!String.IsNullOrEmpty(_errorMessage[3]))
                            return _errorMessage[3];
                        break;
                }
                return string.Empty;
            }
        }
        //validation
        public string Task
        {
            get
            {
                return _newTodo.Task;
            }
            set
            {
                if(_newTodo == null)
                {
                    _newTodo = new ToDo();
                }
                try
                {
                    _newTodo.Task = value;
                    _errorMessage[0] = string.Empty;//validation
                }
                catch (InvalidParameterException ex)
                {
                    _errorMessage[0] = ex.Message;//validation
                }
            }
        }
        public int Difficulty
        {
            get
            {
                return _newTodo.Difficulty;
            }
            set
            {
                if (_newTodo == null)
                {
                    _newTodo = new ToDo();
                }
                try
                {
                    _newTodo.Difficulty = value;
                    _errorMessage[1] = string.Empty;
                }
                catch (InvalidParameterException ex)
                {
                    _errorMessage[1] = ex.Message;
                }
            }
        }
        public DateTime DueDate
        {
            get
            {
                return _newTodo.DueDate;
            }
            set
            {
                if (_newTodo == null)
                {
                    _newTodo = new ToDo();
                }
                try
                {
                    _newTodo.DueDate = value;
                    _errorMessage[2] = string.Empty;
                }
                catch (InvalidParameterException ex)
                {
                    _errorMessage[2] = ex.Message;
                }
            }
        }
        public STATUS Status
        {
            get
            {
                return _newTodo.Status;
            }
            set
            {
                if (_newTodo == null)
                {
                    _newTodo = new ToDo();
                }
                try
                {
                    _newTodo.Status = value;
                    _errorMessage[3] = string.Empty;
                }
                catch (InvalidParameterException ex)
                {
                    _errorMessage[3] = ex.Message;
                }
            }
        }
        public ToDo NewTodo
        {
            get
            {
                return _newTodo;
            }
        }
        public void OnAdd()
        {
            try
            {
                database.Todo.Add(NewTodo);
                database.SaveChanges();
                Task = String.Empty;
                Difficulty = 1;
                DueDate = DateTime.Now;
                Status = STATUS.Pending;
                LoadToDos();
            }
            catch (Exception ex)
                when ((ex is InvalidParameterException) || (ex is SystemException))
            {
                Log.WriteLine(ex.Message);
            }
        }
        public bool CanAdd()
        {
            bool isPropertyFilledCorrectly = true;
            //FIXME: add logic to check whether all fields are properly set
            foreach ( var s in _errorMessage)
            {
                isPropertyFilledCorrectly = isPropertyFilledCorrectly && String.IsNullOrEmpty(s);
            }
            if (NewTodo != null && isPropertyFilledCorrectly)
            {
                return true;
            }
            return false;
        }
    }
}
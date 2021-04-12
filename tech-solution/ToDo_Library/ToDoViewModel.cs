using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ToDoMVVM.Library
{
    public class ToDoViewModel
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
            AddCommand = new ToDoCommand(this.OnAdd, this.CanExecute);
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
        public void OnAdd()
        {
            try
            {
                //WORKROUND: wdit a selected item to Add
                //TOFIX: add item cannot reuse controller bound with list view
                database.Todo.Add(SelectedTodo);
                database.SaveChanges();
                LoadToDos();
            }
            catch (Exception ex)
                when ((ex is InvalidParameterException) || (ex is SystemException))
            {
                Log.WriteLine(ex.Message);
            }
        }
    }
}
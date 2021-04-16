using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZYLibrary;

namespace YZYStudentGUI
{
    //class PaymentViewModel
    //{
    //    private YZYDbContext ctx;
    //    public ObservableCollection<Payment> Payments { get; set; }
    //    public YZYCommand DeleteCommand { get; set; }
    //    public YZYCommand UpdateCommand { get; set; }
    //    public YZYCommand AddCommand { get; set; }

    //    public PaymentViewModel()
    //    {
    //        Log.setLogOnFile();
    //        try
    //        {
    //            ctx = new YZYDbContext();
    //        }
    //        catch (SystemException ex)
    //        {
    //            Log.WriteLine(ex.Message);
    //            Environment.Exit(1);
    //        }
    //        Payments = new ObservableCollection<Payment>();
    //        LoadPayments();
    //        DeleteCommand = new YZYCommand(this.OnDelete, this.CanExecute);
    //        UpdateCommand = new YZYCommand(this.OnUpdate, this.CanExecute);
    //        AddCommand = new YZYCommand(this.OnAdd, this.CanExecute);
    //    }

    //    private void LoadPayments()
    //    {
    //        try
    //        {

    //            var PaymentList = ctx.Payments.ToList();
    //            Payments.Clear();
    //            foreach (var item in PaymentList)
    //            {
    //                Payments.Add(item);
    //            }
    //        }
    //        catch (SystemException ex)
    //        {
    //            Log.WriteLine(ex.Message);
    //        }
    //    }

    //    private Payment _selectedPayment;
    //    public Payment SelectedPayment
    //    {
    //        get
    //        {
    //            return _selectedPayment;
    //        }
    //        set
    //        {
    //            _selectedPayment = value;
    //        }
    //    }

    //    public void OnDelete()
    //    {
    //        ctx.Payments.Remove(SelectedPayment);
    //        ctx.SaveChanges();
    //        LoadPayments();
    //    }
    //    public bool CanExecute()
    //    {
    //        if (SelectedPayment != null)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //    public void OnUpdate()
    //    {
    //        try
    //        {
    //            var item = (from r in ctx.Payments where r.PaymentID == SelectedPayment.PaymentID select r).FirstOrDefault<Payment>();
    //            if (item != null)
    //            {
    //                item.UserID = SelectedPayment.UserID;
    //                item.PayAccount = SelectedPayment.PayAccount;
    //                item.Amount = SelectedPayment.Amount;
    //                item.PayDate = SelectedPayment.PayDate;
    //            }
    //            ctx.SaveChanges();
    //            LoadPayments();
    //        }
    //        catch (Exception ex)
    //            when ((ex is InvalidParameterException) || (ex is SystemException))
    //        {
    //            Log.WriteLine(ex.Message);
    //        }
    //    }
    //    public void OnAdd()
    //    {
    //        try
    //        {
    //            //WORKROUND: wdit a selected item to Add
    //            //TOFIX: add item cannot reuse controller bound with list view
    //            ctx.Payments.Add(SelectedPayment);
    //            ctx.SaveChanges();
    //            LoadPayments();
    //        }
    //        catch (Exception ex)
    //            when ((ex is InvalidParameterException) || (ex is SystemException))
    //        {
    //            Log.WriteLine(ex.Message);
    //        }
    //    }

    //}
}

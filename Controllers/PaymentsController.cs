using ApartmentsRentalManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Web.Mvc;

namespace ApartmentRentalManagement.Controllers
{
    public class PaymentsController : Controller
    {
        BusinessObjects objects = new BusinessObjects();

        // GET: Payments
        public ActionResult Index()
        {
            return View(objects.GetPayments());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Payment rentPayment = objects.FindPayment(id);

            if (rentPayment == null)
            {
                return HttpNotFound();
            }

            return View(rentPayment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(objects.GetEmployees(), "EmployeeID", "Identification");
            ViewBag.RentalContractID = new SelectList(objects.GetRentalContracts(), "RentalContractID", "Description");

            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (SqlConnection scApartmentsManagement = new SqlConnection(System.
                                                                                Configuration.
                                                                                ConfigurationManager.
                                                                                ConnectionStrings["csApartmentsRentalManagement"].
                                                                                ConnectionString))
                {
                    SqlCommand cmdPayments = new SqlCommand("INSERT INTO Management.Payments(ReceiptNumber, EmployeeID, " +
                                                            "                                RentalContractID, PaymentDate, " +
                                                            "                                Amount, Notes) " +
                                                            "VALUES(" + collection["ReceiptNumber"] + ", " +
                                                            collection["EmployeeID"] + ", " + collection["RentalContractID"] +
                                                            ", N'" + collection["PaymentDate"] + "', " + collection["Amount"] +
                                                            ", N'" + collection["Notes"] + "');",
                                                            scApartmentsManagement);

                    scApartmentsManagement.Open();
                    cmdPayments.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Payment pmt = objects.FindPayment(id);

            if (pmt == null)
            {
                return HttpNotFound();
            }

            ViewBag.EmployeeID = new SelectList(objects.GetEmployees(), "EmployeeID", "Identification", pmt.EmployeeID);
            ViewBag.RentalContractID = new SelectList(objects.GetRentalContracts(), "RentalContractID", "Description", pmt.RentalContractID);

            return View(pmt);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                using (SqlConnection scRentManagement = new SqlConnection(System.Configuration.
                                                                                 ConfigurationManager.
                                                                                 ConnectionStrings["csApartmentsRentalManagement"].
                                                                                 ConnectionString))
                {
                    SqlCommand cmdApartments = new SqlCommand("UPDATE Management.Payments " +
                                                              "SET   ReceiptNumber  =   " + collection["ReceiptNumber"] + ",  " +
                                                              "      EmployeeID     =   " + collection["EmployeeID"] + ",  " +
                                                              "      RentalContractID =   " + collection["RentalContractID"] + ",  " +
                                                              "      PaymentDate    = N'" + collection["PaymentDate"] + "', " +
                                                              "      Amount         =   " + collection["Amount"] + ",  " +
                                                              "      Notes          = N'" + collection["Notes"] + "'  " +
                                                              "WHERE PaymentID     =    " + id + ";  ",
                                                             scRentManagement);

                    scRentManagement.Open();
                    cmdApartments.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Payment pmt = objects.FindPayment(id);

            if (pmt == null)
            {
                return HttpNotFound();
            }

            return View(pmt);
        }

        // POST: Payments/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (SqlConnection scRentManagement = new SqlConnection(System.Configuration.
                                                                                 ConfigurationManager.
                                                                                 ConnectionStrings["csApartmentsRentalManagement"].
                                                                                 ConnectionString))
                {
                    SqlCommand cmdPayments = new SqlCommand("DELETE FROM Management.Payments " +
                                                             "WHERE PaymentID = " + id + ";",
                                                             scRentManagement);

                    scRentManagement.Open();
                    cmdPayments.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public List<Payment> GetPayments()
        {
            List<Payment> payments = new List<Payment>();

            using (SqlConnection scApartmentsManagement = new SqlConnection(System.
                                                                            Configuration.
                                                                            ConfigurationManager.
                                                                            ConnectionStrings["csApartmentsRentalManagement"].
                                                                            ConnectionString))
            {
                SqlCommand cmdPayments = new SqlCommand("SELECT PaymentID, ReceiptNumber, EmployeeID, " +
                                                        "       RentalContractID, PaymentDate, " +
                                                        "       Amount, Notes " +
                                                        "FROM   Management.Payments;",
                                                        scApartmentsManagement);

                scApartmentsManagement.Open();
                cmdPayments.ExecuteNonQuery();

                SqlDataAdapter sdaPayments = new SqlDataAdapter(cmdPayments);
                DataSet dsPayments = new DataSet("payments");

                sdaPayments.Fill(dsPayments);

                for (int i = 0; i < dsPayments.Tables[0].Rows.Count; i++)
                {
                    payments.Add(new Payment()
                    {
                        PaymentID = int.Parse(dsPayments.Tables[0].Rows[i][0].ToString()),
                        ReceiptNumber = int.Parse(dsPayments.Tables[0].Rows[i][1].ToString()),
                        EmployeeID = int.Parse(dsPayments.Tables[0].Rows[i][2].ToString()),
                        RentalContractID = int.Parse(dsPayments.Tables[0].Rows[i][3].ToString()),
                        PaymentDate = DateTime.Parse(dsPayments.Tables[0].Rows[i][4].ToString()),
                        Amount = int.Parse(dsPayments.Tables[0].Rows[i][5].ToString()),
                        Notes = dsPayments.Tables[0].Rows[i][6].ToString()
                    });
                }
            }

            return payments;
        }

        public Payment FindPayment(int? id)
        {
            Payment payment = null;

            foreach (var invoice in GetPayments())
            {
                if (invoice.PaymentID == id)
                {
                    payment = invoice;
                    break;
                }
            }

            return payment;
        }
    }
}
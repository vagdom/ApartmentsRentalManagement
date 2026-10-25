using ApartmentsRentalManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ApartmentsRentalManagement.Models
{
    public class BusinessObjects
    {
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection scApartmentsManagement = new SqlConnection(System.
                                                                            Configuration.
                                                                            ConfigurationManager.
                                                                            ConnectionStrings["csApartmentsRentalManagement"].
                                                                            ConnectionString))
            {
                SqlCommand cmdEmployees = new SqlCommand("SELECT EmployeeID, EmployeeNumber, " +
                                                         "       FirstName, LastName, EmploymentTitle " +
                                                         "FROM HumanResources.Employees;",
                                                         scApartmentsManagement);

                scApartmentsManagement.Open();
                cmdEmployees.ExecuteNonQuery();

                SqlDataAdapter sdaEmployees = new SqlDataAdapter(cmdEmployees);
                DataSet dsEmployees = new DataSet("employees");

                sdaEmployees.Fill(dsEmployees);

                Employee staff = null;

                for (int i = 0; i < dsEmployees.Tables[0].Rows.Count; i++)
                {
                    DataRow drEmployee = dsEmployees.Tables[0].Rows[i];

                    staff = new Employee()
                    {
                        EmployeeID = int.Parse(drEmployee[0].ToString()),
                        EmployeeNumber = drEmployee[1].ToString(),
                        FirstName = drEmployee[2].ToString(),
                        LastName = drEmployee[3].ToString(),
                        EmploymentTitle = drEmployee[4].ToString()
                    };

                    employees.Add(staff);
                }
            }

            return employees;
        }

        public Employee FindEmployee(int? id)
        {
            Employee employee = null;

            foreach (var staff in GetEmployees())
            {
                if (staff.EmployeeID == id)
                {
                    employee = staff;
                    break;
                }
            }

            return employee;
        }

        public List<Apartment> GetApartments()
        {
            List<Apartment> apartments = new List<Apartment>();

            using (SqlConnection scApartmentsManagement = new SqlConnection(System.
                                                                            Configuration.
                                                                            ConfigurationManager.
                                                                            ConnectionStrings["csApartmentsRentalManagement"].
                                                                            ConnectionString))
            {
                SqlCommand cmdApartments = new SqlCommand("SELECT ApartmentID, UnitNumber, Bedrooms, " +
                                                          "       Bathrooms, MonthlyRate, " +
                                                          "       SecurityDeposit, OccupancyStatus " +
                                                          "FROM Management.Apartments;",
                                                          scApartmentsManagement);

                scApartmentsManagement.Open();
                cmdApartments.ExecuteNonQuery();

                SqlDataAdapter sdaApartments = new SqlDataAdapter(cmdApartments);
                DataSet dsApartments = new DataSet("apartments");

                sdaApartments.Fill(dsApartments);

                for (int i = 0; i < dsApartments.Tables[0].Rows.Count; i++)
                {
                    DataRow drApartment = dsApartments.Tables[0].Rows[i];

                    Apartment unit = new Apartment()
                    {
                        ApartmentID = int.Parse(drApartment[0].ToString()),
                        UnitNumber = drApartment[1].ToString(),
                        Bedrooms = int.Parse(drApartment[2].ToString()),
                        Bathrooms = int.Parse(drApartment[3].ToString()),
                        MonthlyRate = int.Parse(drApartment[4].ToString()),
                        SecurityDeposit = int.Parse(drApartment[5].ToString()),
                        OccupancyStatus = drApartment[6].ToString()
                    };

                    apartments.Add(unit);
                }
            }

            return apartments;
        }

        public List<RentalContract> GetRentalContracts()
        {
            List<RentalContract> rentalContracts = new List<RentalContract>();

            using (SqlConnection scApartmentsManagement = new SqlConnection(System.
                                                                            Configuration.
                                                                            ConfigurationManager.
                                                                            ConnectionStrings["csApartmentsRentalManagement"].
                                                                            ConnectionString))
            {
                SqlCommand cmdRentalContracts = new SqlCommand("SELECT RentalContractID, ContractNumber, EmployeeID, " +
                                                             "       ContractDate, FirstName, LastName, " +
                                                             "       MaritalStatus, NumberOfChildren, " +
                                                             "       ApartmentID, RentStartDate " +
                                                             "FROM   Management.RentalContracts;",
                                                             scApartmentsManagement);

                scApartmentsManagement.Open();
                cmdRentalContracts.ExecuteNonQuery();

                SqlDataAdapter sdaRentalContracts = new SqlDataAdapter(cmdRentalContracts);
                DataSet dsRentalContracts = new DataSet("rent-contracts");

                sdaRentalContracts.Fill(dsRentalContracts);

                for (int i = 0; i < dsRentalContracts.Tables[0].Rows.Count; i++)
                {
                    RentalContract contract = new RentalContract()
                    {
                        //ContractNumber = int.Parse(dsRentalContracts.Tables[0].Rows[i][1].ToString()),
                        RentalContractID = int.Parse(dsRentalContracts.Tables[0].Rows[i][1].ToString()),
                        EmployeeID = int.Parse(dsRentalContracts.Tables[0].Rows[i][2].ToString()),
                        ContractDate = DateTime.Parse(dsRentalContracts.Tables[0].Rows[i][3].ToString()),
                        FirstName = dsRentalContracts.Tables[0].Rows[i][4].ToString(),
                        LastName = dsRentalContracts.Tables[0].Rows[i][5].ToString(),
                        MaritalStatus = dsRentalContracts.Tables[0].Rows[i][6].ToString(),
                        NumberOfChildren = int.Parse(dsRentalContracts.Tables[0].Rows[i][7].ToString()),
                        ApartmentID = int.Parse(dsRentalContracts.Tables[0].Rows[i][8].ToString()),
                        RentStartDate = DateTime.Parse(dsRentalContracts.Tables[0].Rows[i][9].ToString())
                    };

                    rentalContracts.Add(contract);
                }
            }

            return rentalContracts;
        }

        public RentalContract FindRentalContract(int? id)
        {
            RentalContract contract = null;

            foreach (var rent in GetRentalContracts())
            {
                if (rent.RentalContractID == id)
                {
                    contract = rent;
                    break;
                }
            }

            return contract;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Contracts;
using TRLAFCoSys.Logic.Models;
using TRLAFCoSys.Queries.Core.Domain;
using TRLAFCoSys.Queries.Persistence;

namespace TRLAFCoSys.Logic.Implementors
{
    public class MilkUtilizeLogic : IMilkUtilizeLogic
    {
        public MilkUtilizeLogic() { }

        public void Add(MilkUtilizeAddModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var objs = uow.MilkUtilizeRecords.GetAllBy(model.ActualDate);
                    if (objs.Count() != 0)
                    {
                        throw new ApplicationException("Current date " + DateTime.Now.ToLongDateString() + " already exists!");
                    }
                    var obj = new MilkUtilizeRecord();
                    obj.ActualDate = model.ActualDate;
                    obj.Analysis = model.Analysis;
                    obj.CreateDateTime = DateTime.Now;
                    obj.Demo = model.Demo;
                    obj.RawMilkProcess = model.RawMilkProcess;
                    obj.Remarks = model.Remarks;
                    obj.Spillage = model.Spillage;
                    obj.SpoilageQty = model.SpoilageQty;
                    obj.SpoilageValue = model.SpoilageValue;
                    obj.WithdrawnByProcessor = model.WithdrawnByProcessor;
                    obj.Remarks = model.Remarks;
                    uow.MilkUtilizeRecords.Add(obj);
                    uow.Complete();
                    model.ID = obj.MilkUtilizeRecordID;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                   
                    var obj = uow.MilkUtilizeRecords.Get(id);
                    uow.MilkUtilizeRecords.Remove(obj);
                    uow.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<MilkUtilizeListModel> GetRecords(DateTime date)
        {
            try
            {
                var models = new List<MilkUtilizeListModel>();
                using (var uow = new UnitOfWork(new DataContext()))
                {

                    var objs = this.GetComputedList().Where(r => r.ActualDate.Month == date.Month && r.ActualDate.Year == date.Year).ToList();
                    //Get Beginning Volume of records
                    
                    foreach (var item in objs)
                    {
                        // add to model
                        var model = new MilkUtilizeListModel();
                        model.ID = item.ID;
                        model.ActualDate = item.ActualDate;
                        model.Analysis = item.Analysis;
                        model.BeginningVolume = item.BeginningVolumeBalance;
                        model.EndingVolumeBalance = item.EndingVolumeBalance;
                        model.RawMilkProcess = item.RawMilkProcess;
                        model.TotalRawMilkWithdrawn = item.TotalMilkWithdrawn;
                        model.TotalMilkForUtilization = item.TotalMilkForUtilization;
                        model.MilkDeliveredFromFarmers = item.MilkDeliveredByFarmers;
                        model.Spillage = item.Spillage;
                        model.Analysis = item.Analysis;
                        models.Add(model);
                    }
                }
                return models.Where(r => r.ActualDate.Month == date.Month);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void Edit(int id, MilkUtilizeEditModel model)
        {
             try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.MilkUtilizeRecords.Get(id);
                    //obj.ActualDate = model.ActualDate;
                    obj.Analysis = model.Analysis;
                    obj.Demo = model.Demo;
                    obj.RawMilkProcess = model.RawMilkProcess;
                    obj.Remarks = model.Remarks;
                    obj.Spillage = model.Spillage;
                    obj.SpoilageQty = model.SpoilageQty;
                    obj.SpoilageValue = model.SpoilageValue;
                    obj.WithdrawnByProcessor = model.WithdrawnByProcessor;
                    obj.Remarks = model.Remarks;
                    uow.MilkUtilizeRecords.Edit(obj);
                    uow.Complete();
                }
                   
            }
            catch (Exception)
            {

                throw;
            }
        }

        public MilkUtilizeModel GetRecord(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    

                    var model = new MilkUtilizeModel();

                    // get second to the last record before current date

                    var obj = this.GetComputedList().FirstOrDefault(x => x.ID == id);
                    model.ActualDate = obj.ActualDate;
                    model.Analysis = obj.Analysis;
                    model.BeginningVolumeBalance = obj.BeginningVolumeBalance;
                    model.Demo = obj.Demo;
                    model.EndingVolumeBalance = obj.EndingVolumeBalance;
                    model.TotalMilkDeliveredFromFarmers = obj.MilkDeliveredByFarmers;
                    model.RawMilkProcess = obj.RawMilkProcess;
                    model.RawMilkSold = obj.TotalMilkSold;
                    model.Remarks = obj.Remarks;
                    model.Spillage = obj.Spillage;
                    model.SpoilageQty = obj.SpoilageQty;
                    model.SpoilageValue = obj.SpoilageValue;
                    model.TotalMilkForUtilization = obj.TotalMilkForUtilization;
                    model.TotalRawMilkWithdrawn = obj.TotalMilkWithdrawn;
                    model.WithdrawnByProcessor = obj.WithdrawnByProcessor;
                    return model;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public MilkUtilizeResultModel Compute(MilkUtilizeComputeModel model)
        {
            var totalMilkWitdrawn = model.RawMilkProcess + model.TotalMilkSold;
            var endingBalance = model.TotalMilkForUtilization - totalMilkWitdrawn - model.Spillage - model.Analysis;

            return new MilkUtilizeResultModel { EndingBalance = endingBalance, TotalRawMilkWithdrawn = totalMilkWitdrawn };
        }

        private IEnumerable<MilkUtilizeComputeModel> GetComputedList()
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {

                    //Get Beginning Volume of records
                    double beginningBalance = 0.0;
                    double totalMilkForUtilization = 0.0;
                    double rawMilkProcess = 0.0;
                    double totalRawMilkWithdrawn = 0.0;
                    double spillage = 0.0;
                    double analysis = 0.0;
                    double endingBalance = 0.0;

                    //Compute for beginning and ending results of every record
                    var objs = uow.MilkUtilizeRecords.GetAllRecords();
                    var computeList = new List<MilkUtilizeComputeModel>();
                    foreach (var item in objs)
                    {
                        double totalMilkSoldToCustomers = 0.0;
                        double totalMilkDelivered = 0.0;
                        // previous ending balance = beginning balance;
                        beginningBalance = endingBalance;

                        //Get Total Milk Collections on current loop date
                        totalMilkDelivered = uow.MilkCollections.GetAllBy(item.ActualDate).Sum(x => x.Volume);

                        totalMilkForUtilization = beginningBalance + totalMilkDelivered;

                        //Get Total Milk Sold for the current loop date
                        totalMilkSoldToCustomers = uow.ProductSales.GetAllBy(item.ActualDate, "Raw Milk").Sum(x => x.Quantity);

                        //Get Total Raw Milk Process from Production per day
                        rawMilkProcess = uow.Productions.GetProducts(item.ActualDate, isProduce: true)
                            .Select( x => new {rawMilkProcess = x.Product.ProductRawMaterials.Where(n => n.SupplyType.SupplyClass.Description == "Raw Milk")
                                .Sum(y => y.Quantity * x.Quantity)}).Sum(b => b.rawMilkProcess);

                        totalRawMilkWithdrawn = totalMilkSoldToCustomers + rawMilkProcess;

                        spillage = item.Spillage;
                        analysis = item.Analysis;
                        // compute for ending balance
                        endingBalance = totalMilkForUtilization - totalRawMilkWithdrawn - spillage - analysis;

                        var compute = new MilkUtilizeComputeModel();
                        compute.ID = item.MilkUtilizeRecordID;
                        compute.ActualDate = item.ActualDate;
                        compute.Analysis = analysis;
                        compute.RawMilkProcess = rawMilkProcess;
                        compute.TotalMilkSold = totalMilkSoldToCustomers;
                        compute.Spillage = spillage;
                        compute.TotalMilkForUtilization = totalMilkForUtilization;
                        compute.BeginningVolumeBalance = beginningBalance;
                        compute.EndingVolumeBalance = endingBalance;
                        compute.MilkDeliveredByFarmers = totalMilkDelivered;
                        compute.TotalMilkWithdrawn = totalRawMilkWithdrawn;
                        compute.Demo = item.Demo;
                        compute.SpoilageQty = item.SpoilageQty;
                        compute.SpoilageValue = item.SpoilageValue;
                        compute.WithdrawnByProcessor = item.WithdrawnByProcessor;
                        compute.Remarks = item.Remarks;
                        computeList.Add(compute);

                    }
                    return computeList;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public IEnumerable<MilkUtilizeComputeModel> GetComputedList2()
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {

                    //Get Beginning Volume of records
                    double beginningBalance = 0.0;
                    double totalMilkForUtilization = 0.0;
                    double rawMilkProcessed = 0.0;
                    double totalRawMilkWithdrawn = 0.0;
                    double spillage = 0.0;
                    double analysis = 0.0;
                    double endingBalance = 0.0;

                    /**Get all milk collections, checks for milk utilization for particalar milk collection date
                     * compute for beginning and endings
                     */
                    var milkCollections = uow.MilkCollections.GetAllRecords()
                        .OrderBy(x => x.ActualDate)
                        .GroupBy(x => x.ActualDate)
                        .Select(x => new {TotalMilkCollectionPerDay = x.Sum(y => y.Volume), ActualDate = x.First().ActualDate}).ToList();
                    var computeList = new List<MilkUtilizeComputeModel>();
                    foreach (var milkCollection in milkCollections)
                    {
                        var loopDate = milkCollection.ActualDate;
                        double totalMilkSold = 0.0;
                        double milkDelivered = 0.0;
                        // previous ending balance = beginning balance;
                        beginningBalance = endingBalance;


                        milkDelivered = milkCollection.TotalMilkCollectionPerDay;

                        //get total milk collections by current loop date
                        
                        totalMilkForUtilization = beginningBalance + milkCollection.TotalMilkCollectionPerDay;
                        // get total sold from customers

                        //var actualDailySales = uow.DailySaleRecords.GetAllBy(loopDate)
                        //    .GroupBy(x => x.CreateDateTime)
                        //    .Select(x => new {TotalRawMilkSold = x.Sum(y => y.};

                        var milkDeliveryAndUtilization = uow.MilkUtilizeRecords.GetAllBy(loopDate)
                            .GroupBy(x => x.ActualDate)
                            .Select(x => new
                            {
                                TotalRawMilkProcess = x.Sum(y => y.RawMilkProcess),
                                TotalSpillage = x.Sum(y => y.Spillage),
                                TotalAnalysis = x.Sum(y => y.Analysis)
                            }).FirstOrDefault();

                        if (milkDeliveryAndUtilization != null)
                        {
                            rawMilkProcessed = milkDeliveryAndUtilization.TotalRawMilkProcess;
                            spillage = milkDeliveryAndUtilization.TotalSpillage;
                            analysis = milkDeliveryAndUtilization.TotalAnalysis;
                        }
                            
                            // compute for ending balance
                            totalRawMilkWithdrawn = totalMilkSold + rawMilkProcessed;
                            endingBalance = totalMilkForUtilization - totalRawMilkWithdrawn - spillage - analysis;
                        
                       

                        var compute = new MilkUtilizeComputeModel();
                        
                        compute.ActualDate = milkCollection.ActualDate;
                        compute.Analysis = analysis;
                        compute.RawMilkProcess = rawMilkProcessed;
                        compute.TotalMilkSold = totalMilkSold;
                        compute.Spillage = spillage;
                        compute.TotalMilkForUtilization = totalMilkForUtilization;
                        compute.BeginningVolumeBalance = beginningBalance;
                        compute.EndingVolumeBalance = endingBalance;
                        compute.MilkDeliveredByFarmers = milkDelivered;
                        compute.TotalMilkWithdrawn = totalRawMilkWithdrawn;
                        computeList.Add(compute);

                    }
                    return computeList;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public MilkForUtilizationtModel GetComputedRecordForAdd(DateTime date)
        {
            try
            {
                double milkDelivered = 0.0;
                double totalMilkForUtilization = 0.0;
                double beginningBalance = 0.0;
                // ordered by date
                var obj = this.GetComputedList().FirstOrDefault();
                var model = new MilkForUtilizationtModel();
                beginningBalance = obj.EndingVolumeBalance;
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    //get milk collections by current loop date
                    var finalMilkCollections = uow.MilkCollections.GetAllBy(date).GroupBy(x => x.ActualDate).Select(y => new { TotalVolume = y.Sum(z => z.Volume) });
                    if (finalMilkCollections.Count() != 0)
                    {
                        milkDelivered = finalMilkCollections.FirstOrDefault().TotalVolume;
                    }
                   totalMilkForUtilization = beginningBalance + milkDelivered;
                   

                }
                model.BeginningBalance = beginningBalance;
                model.TotalMilkDeliveredByFarmers = milkDelivered;
                model.TotalMilkForUtilization = totalMilkForUtilization;
                return model;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public MilkForUtilizationtModel GetMilkForUtilization(DateTime date)
        {
            try
            {
                double milkDelivered = 0.0;
                double totalMilkForUtilization = 0.0;
                double beginningBalance = 0.0;
                //this.GetComputedList2();
                var computedList = this.GetComputedList2();
               
                
                //yesterday record
                var beginningBalanceRecord = computedList.Where(r => r.ActualDate.Date < date.Date)
                    .OrderByDescending(r => r.ActualDate).Select(r => new { EndingBalance = r.EndingVolumeBalance, Date = r.ActualDate, MilkCollection = r.MilkDeliveredByFarmers });
                // today record
                var currentBalanceRecord = computedList.Where(r => r.ActualDate.Date == date.Date)
                   .Select(r => new { EndingBalance = r.EndingVolumeBalance, Date = r.ActualDate, MilkCollection = r.MilkDeliveredByFarmers });
                
                 //check if list contains elements
                if (beginningBalanceRecord.Count() > 0)
                {
                    beginningBalance = beginningBalanceRecord.FirstOrDefault().EndingBalance;
                }
                if (currentBalanceRecord.Count() > 0)
                {
                    milkDelivered = currentBalanceRecord.FirstOrDefault().MilkCollection;
                }
                // get total milk for utilization
                // add values
                totalMilkForUtilization = beginningBalance + milkDelivered;
                // create new model then return it
                var model = new MilkForUtilizationtModel();
                model.BeginningBalance = beginningBalance;
                model.TotalMilkDeliveredByFarmers = milkDelivered;
                model.TotalMilkForUtilization = totalMilkForUtilization;
                return model;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public MilkForUtilizationtModel GetMilkForUtilizationV2(DateTime date)
        {
            try
            {
                double milkDelivered = 0.0;
                double totalMilkForUtilization = 0.0;
                double beginningBalance = 0.0;
                //this.GetComputedList2();
                var computedList = this.GetComputedList2();

                //beginningBalance = computedList
                //we get previous and current record from computed record list
                var previousRecord = computedList.Where(r => r.ActualDate.Date < date.Date)
                    .OrderByDescending(r => r.ActualDate).Select(r => new { EndingBalance = r.EndingVolumeBalance, Date = r.ActualDate, MilkCollection = r.MilkDeliveredByFarmers });
                var currentRecord = computedList.Where(r => r.ActualDate.Date == date.Date)
                   .Select(r => new { EndingBalance = r.EndingVolumeBalance, Date = r.ActualDate, MilkCollection = r.MilkDeliveredByFarmers });

                // check if list contains elements
                if (previousRecord.Count() > 0)
                {
                    beginningBalance = previousRecord.FirstOrDefault().EndingBalance;
                }
                if (currentRecord.Count() > 0)
                {
                    milkDelivered = currentRecord.FirstOrDefault().MilkCollection;
                }
                // get total milk for utilization
                // add values
                totalMilkForUtilization = beginningBalance + milkDelivered;
                // create new model then return it
                var model = new MilkForUtilizationtModel();
                model.BeginningBalance = beginningBalance;
                model.TotalMilkDeliveredByFarmers = milkDelivered;
                model.TotalMilkForUtilization = totalMilkForUtilization;
                return model;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public MilkUtilizeSummaryModel GetSummary(DateTime date)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var model = new MilkUtilizeSummaryModel();

                    // get total milk collections for the month
                    var summaries = this.GetComputedList()
                        .Where(r => r.ActualDate.Month == date.Month)
                        .GroupBy(r => r.ActualDate.Month)
                        .Select(r => new { 
                            MilkCollectionPerMonth = r.Sum(x => x.MilkDeliveredByFarmers),
                            MilkSoldPerMonth = r.Sum(x => x.TotalMilkSold),
                            MilkProcessPerMonth = r.Sum(x => x.RawMilkProcess),
                            MilkWithdrawnPerMonth = r.Sum(x => x.TotalMilkWithdrawn),
                            MilkAnalysisPerMonth = r.Sum(x => x.Analysis),
                        });

                   

                    if (summaries.Count() > 0)
                    {
                        var obj = summaries.Single();
                        model.MilkCollectionPerMonth = obj.MilkCollectionPerMonth;
                        model.MilkSoldPerMonth = obj.MilkSoldPerMonth;
                        model.MilkAnalysisPerMonth = obj.MilkAnalysisPerMonth;
                        model.MilkProcessPerMonth = obj.MilkProcessPerMonth;
                        model.MilkWithdrawnPerMonth = obj.MilkWithdrawnPerMonth;
                    }
                   
                    
                    return model;
                }
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public IEnumerable<MilkUtilizeProductSummaryModel> GetProductSummary(DateTime date)
        {
            try
            {
                var models = new List<MilkUtilizeProductSummaryModel>();
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    foreach (var product in uow.MilkUtilizeProducts.GetAll())
                    {
                        var model = new MilkUtilizeProductSummaryModel();
                        model.ProductName = product.Description;
                        var milkUtilizeList = uow.MilkUtilizeProductRecords.GetAllBy(date,product.MilkUtilizeProductID);
                        var objs = milkUtilizeList
                            .GroupBy(r => r.MilkUtilizeProductID)
                            .Select(r => new { Total = r.Sum(x => x.Quantity) });
                        if (objs.Count() > 0)
                        {
                            model.Total = objs.Single().Total;
                        }
                        models.Add(model);
                    }
                    return models;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public MilkUtilizeModel GetRecord(DateTime date)
        {
           
                using (var uow = new UnitOfWork(new DataContext()))
                {


                    var model = new MilkUtilizeModel();

                    // get second to the last record before current date

                    var obj = this.GetComputedList().FirstOrDefault(x => x.ActualDate.Date == date.Date);
                    if (obj != null)
                    {
                        model.ID = obj.ID;
                        model.ActualDate = obj.ActualDate;
                        model.Analysis = obj.Analysis;
                        model.BeginningVolumeBalance = obj.BeginningVolumeBalance;
                        model.Demo = obj.Demo;
                        model.EndingVolumeBalance = obj.EndingVolumeBalance;
                        model.TotalMilkDeliveredFromFarmers = obj.MilkDeliveredByFarmers;
                        model.RawMilkProcess = obj.RawMilkProcess;
                        model.RawMilkSold = obj.TotalMilkSold;
                        model.Remarks = obj.Remarks;
                        model.Spillage = obj.Spillage;
                        model.SpoilageQty = obj.SpoilageQty;
                        model.SpoilageValue = obj.SpoilageValue;
                        model.TotalMilkForUtilization = obj.TotalMilkForUtilization;
                        model.TotalRawMilkWithdrawn = obj.TotalMilkWithdrawn;
                        model.WithdrawnByProcessor = obj.WithdrawnByProcessor;
                    }
                    return model;
                }

           
        }


        public bool CheckRecordIfExists(DateTime date)
        {
            using (var uow = new UnitOfWork(new DataContext()))
            {

                var obj = this.GetComputedList().FirstOrDefault(x => x.ActualDate.Date == date.Date);
                if (obj != null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}

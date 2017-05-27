using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using System.Collections.Generic;
using System.Linq;

/**
* @(#) DocumentData.cs
*/
namespace DrivingSchool.Services
{
    public class DocumentData : DataService<Document>
    {
        public DocumentData(DrivingSchoolDbContext context) : base(context) { }

        public override Document Get(int id) =>
            m_context.Documents.FirstOrDefault(s => s.Id == id);

        public override IQueryable<Document> GetAll() =>
            m_context.Documents;

        public override void RemoveRange(IEnumerable<Document> data) => m_context.Documents.
            RemoveRange(data);
    }

    public static class DocumentDataServiceExtensions
    {
        public static IEnumerable<Document>
            GetCarsDocuments(this IDataService<Document> data, Car car) => data.GetAll().
            Where(p => p.OwnerCar == car).OrderBy(d => d.Type).ThenByDescending(d => d.EndDate);

        public static void UpdateCarsDocuments(this IDataService<Document> data, Car car,
            List<Document> docs)
        {
            //m_context.Database.
            //    ExecuteSqlCommand("DELETE FROM Documents WHERE OwnerCarId = {0}", car.Id);
            data.RemoveRange(data.GetAll().Where(p => p.OwnerCar == car));
            data.SaveChanges();
            car.Documents = docs;
        }
    }
}

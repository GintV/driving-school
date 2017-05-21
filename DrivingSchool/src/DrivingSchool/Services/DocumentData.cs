using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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



        public IQueryable<Document> GetCarsDocuments(Car car)
        {
            return m_context.Documents.Where(p => p.OwnerCar == car);
        }

        public void UpdateCarsDocuments(Car car, List<Document> docs)
        {
            m_context.Database.ExecuteSqlCommand("DELETE FROM Documents WHERE OwnerCarId = {0}", car.Id);
            car.Documents = docs;
        }
    }
}

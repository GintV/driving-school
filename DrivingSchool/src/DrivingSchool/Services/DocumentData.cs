using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using System;
using System.Linq;

/**
* @(#) DocumentData.cs
*/
namespace DrivingSchool.Services
{
    public class DocumentData : Service<Document>
    {
        public DocumentData(DrivingSchoolDbContext context) : base(context) { }

        public override Document Get(int id) =>
            m_context.Documents.FirstOrDefault(s => s.Id == id);

        public override IQueryable<Document> GetAll() =>
            m_context.Documents;



        public void GetCarsDocuments()
        {
            throw new NotImplementedException();
        }

        public void UpdateCarsDocuments()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC_Practice.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public IQueryable<客戶銀行資訊> AllExist()
        {
            return this.All().Where(x => x.是否已刪除 == false).OrderBy(x => x.客戶Id);
        }

        public 客戶銀行資訊 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
	}

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}
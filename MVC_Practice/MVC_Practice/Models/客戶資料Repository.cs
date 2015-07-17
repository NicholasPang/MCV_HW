using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC_Practice.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public IQueryable<客戶資料> AllExist()
        {
            return this.All().Where(x => x.是否已刪除 == false);
        }

        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
	}

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}
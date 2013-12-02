using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpartanSpots.Models;
namespace SpartanSpots.Helpers
{
    public static class HtmlExtensions
    {
        

        public static string LabelBusinessName(int id)
        {
            UsersContext db = new UsersContext();
            Business business = db.Businesses.Find(id);

            return(string.Format("{0}", business.Name));
       
        }

        public static string TotalRating(int id)
        {
            UsersContext db = new UsersContext();
            Business business = db.Businesses.Find(id);
            int numOfReviews = business.Reviews.Count;
            double totalRating = 0.0;
            foreach (Review review in business.Reviews)
                totalRating += review.Rating;

            totalRating = totalRating / numOfReviews;
            return (string.Format("{0} out of 5. {1}", totalRating, numOfReviews));

        }

        public static List<Business> TopThreeRatedRestaurants()
        {
            UsersContext db = new UsersContext();
            
            var m = db.Businesses.Where(x => x.Category.Contains("Restaurant"));
            List<Business> r = m.OrderByDescending(x => x.TotalRating).Take(3).ToList(); 
            return r;
        }
        public static List<Business> TopThreeRatedBars()
        {
            UsersContext db = new UsersContext();

            var m = db.Businesses.Where(x => x.Category.Contains("Bar"));
            List<Business> r = m.OrderByDescending(x => x.TotalRating).Take(3).ToList();
            return r;
        }
    }
}
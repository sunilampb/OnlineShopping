using DataLayer.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AdRepository : IAdRepository
    {
        private string conString;
        public AdRepository()
        {
            conString = ConfigurationManager.ConnectionStrings["myConString"].ConnectionString;
        }
        public AdRepository(string connectionString)
        {
            conString = connectionString;
        }
        public int InsertAd(DTO.Ad ad)
        {
            try
            {
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("InsertAd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Title", ad.Title);
                    cmd.Parameters.AddWithValue("Description", ad.Description);
                    cmd.Parameters.AddWithValue("City", ad.City);
                    cmd.Parameters.AddWithValue("Locality", ad.Locality);
                    cmd.Parameters.AddWithValue("Price", ad.Price);
                    cmd.Parameters.AddWithValue("ValidTill", ad.ValidTill);
                    cmd.Parameters.AddWithValue("CategoryId", ad.CategoryId);
                    cmd.Parameters.AddWithValue("UserId", ad.UserId);
                    decimal newAdId = (decimal)cmd.ExecuteScalar();
                    return Convert.ToInt32(newAdId); 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DTO.Category> GetCategories()
        {
            try
            {
                var list = new List<DTO.Category>();
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("select * from category", con);
                    var rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        var cat = new Category();
                        cat.CategoryId = (int)rdr["CategoryId"];
                        cat.CategoryName = (string)rdr["Name"];
                        list.Add(cat);
                    }
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DTO.Ad> GetAllAds()
        {
            throw new NotImplementedException();
        }

        public List<DTO.Ad> SearchAds(string searchTerm, int ?categoryId, string city, string locality, int priceLow, int priceHigh, string sortBy)
        {
            try
            {
                List<DTO.Ad> ads = new List<Ad>(); ;
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("SearchAd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("searchTerm", searchTerm);
                    cmd.Parameters.AddWithValue("CategoryId", categoryId);
                    cmd.Parameters.AddWithValue("City", city);
                    cmd.Parameters.AddWithValue("Locality", locality);
                    cmd.Parameters.AddWithValue("PriceLow", priceLow);
                    cmd.Parameters.AddWithValue("PriceHigh", priceHigh);
                    cmd.Parameters.AddWithValue("sortBy", sortBy);
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var ad = new Ad();
                        ad.AdId = (int)rdr["AdId"];
                        ad.CategoryId = (int)rdr["CategoryId"];
                        ad.City = (string)rdr["City"];
                        ad.Description = (string)rdr["Description"];
                        ad.Locality = (string)rdr["Locality"];
                        ad.PostedDate = (DateTime)rdr["PostedDate"];
                        ad.Price = (int)rdr["Price"];
                        ad.Title = (string)rdr["Title"];
                        ad.UserId = (int)rdr["UserId"];
                        ad.ValidTill = (DateTime)rdr["ValidTill"];
                        ads.Add(ad);
                    }
                }
                return ads;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Ad GetAdById(int adId)
        {
            try
            {
                DTO.Ad ad = null; ;
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("GetAdById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AdId", adId);
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        ad = new Ad();
                        ad.AdId = (int)rdr["AdId"];
                        ad.CategoryId = (int)rdr["CategoryId"];
                        ad.City = (string)rdr["City"];
                        ad.Description = (string)rdr["Description"];
                        ad.Locality = (string)rdr["Locality"];
                        ad.PostedDate = (DateTime)rdr["PostedDate"];
                        ad.Price = (int)rdr["Price"];
                        ad.Title = (string)rdr["Title"];
                        ad.UserId = (int)rdr["UserId"];
                        ad.ValidTill = (DateTime)rdr["ValidTill"];
                        ad.AdImages = GetAdImages(adId);
                    }
                }
                return ad;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int SaveAdImages(List<byte[]> images, int adId)
        {
            try
            {
                int rowsAffected = 0;
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    foreach (var image in images)
                    {
                        var cmd = new SqlCommand("SaveAdImages", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("ImageData", image);
                        cmd.Parameters.AddWithValue("AdId", adId);
                        rowsAffected += cmd.ExecuteNonQuery();
                    }
                }
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<byte[]> GetAdImages(int adId)
        {
            try
            {
                List<byte[]> images = new List<byte[]>(); ;
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("GetAdImages", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AdId", adId);
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var bytes = (byte[])rdr["ImageData"];
                        images.Add(bytes);
                    }
                }
                return images;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using NoScrip.Lib;
using NoScrip.Models;

public class DefaultData
{
    public static string[] GENRES = new string[]{
     "action",
     "adventure",
     "chuyển sinh",
     "comic",
     "cổ đại",
     "drama",
     "ecchi",
     "gender bender",
     "historical",
     "josei",
      "manhwa",
      "mature",
      "mystery",
      "one shot",
      "romance",
      "sci-fi",
      "shounen",
      "slice of Life",
      "soft yaoi",
      "sport",
      "việt nam",
      "xuyên không",
      "adult",
      "comedy",
      "cooking",
      "doujinshi",
      "đam mỹ",
      "fantasy",
      "harem",
      "horror",
      "live action",
      "manhua",
      "martial arts",
      "mecha",
      "ngôn  tình",
      "psychological",
      "school life",
      "seinen",
      "shoujo ai",
      "smut",
      "soft yuri",
      "supermatural",
      "trinh thám",
      "truyện màu"
     };
    public static string[] BOOK_STATUS = new string[]{
      "đang tiến hành","đã hoàn thành"
     };
    public static string[] DOMAIN = new string[]{
      "www.nettruyenin.com",
      "nhattruyenin.com",
      "truyenqqpro.com",
      "manhuarock.net"
     };
    public static IEnumerable<Genres> GetGenres()
    {

        foreach (var item in GENRES)
        {
            yield return new Genres()
            {
                Tag = item
            };
        }

    }
    // public static IEnumerable<Status> GetBookStatus()
    // {

    //     foreach (var item in BOOK_STATUS)
    //     {
    //         yield return new Status()
    //         { Name = item };
    //     }


    // }
    public static IEnumerable<Server> GetServer()
    {

        foreach (var item in DOMAIN)
        {
            yield return new Server()
            { Domain = item, Name = "awn" };
        }

    }
}

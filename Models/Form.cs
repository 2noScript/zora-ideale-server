namespace NoScrip.Models;


public class ImageForm
{
    public int index { get; set; }

    public string? Url { get; set; }
}

public class BookForm
{

    public class Avatar
    {
        public int ServerId { get; set; }
        public string? Url { get; set; }

    }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public BookStatus BookStatus { get; set; }
    public List<Avatar>? Avatars { get; set; }

}



public class chapterForm
{

}

public class ServerForm
{
    public string? Name { get; set; }
    public string? Domain { get; set; }

}





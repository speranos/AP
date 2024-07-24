public class Todo {
    public Guid id {get; set;} = new Guid();
    public string content {get; set;}
    public string status {get; set;} = "in progress";

}
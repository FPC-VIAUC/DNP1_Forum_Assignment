namespace CustomExceptions;

public class NotFoundException : InvalidOperationException{
    public NotFoundException(){
    }

    public NotFoundException(string message) : base(message){
    }

    public NotFoundException(string message, Exception inner) : base(message, inner){
    }
}
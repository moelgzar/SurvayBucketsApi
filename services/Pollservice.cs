
using SurvayBucketsApi.Models;

namespace SurvayBucketsApi.services;

public class Pollservice : IPollservice
{
    private readonly static List<Poll> _polls = [

        new Poll { 
            id = 1, Title = "Favorite Programming Language",
            Description = "Vote for your favorite programming language."  
        } , 
        new Poll {
            id = 2,
            Title = "gaazar" ,
            Description = "sdf"
        }

];

   

    public IEnumerable<Poll> GetAll()
    {
        return _polls;
    }
   
    public Poll AddPoll(Poll poll)
    {
        poll.id = _polls.Count + 1;
        _polls.Add(poll);

        return poll;
    }

    public bool UpdatePoll(int id  ,Poll poll)
    {
       var currentpoll = GetPollById(id);
        if(currentpoll is null) 
          return false;
        

        currentpoll.Title = poll.Title;
        currentpoll.Description = poll.Description;

        return true;
    }

    public Poll? GetPollById(int id)
    {
        return _polls.SingleOrDefault(d => d.id == id);

    }

    public bool DeletePoll(int id)
    {
       var poll = GetPollById(id);
        if(poll is null)
            return false;
        _polls.Remove(poll);
        return true;
    }
}

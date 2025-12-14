namespace SurvayBucketsApi.services;

public interface IPollservice
{
    IEnumerable<Poll> GetAll();
     Poll? GetPollById(int id);
     Poll AddPoll(Poll poll);
    bool UpdatePoll(int id , Poll poll);
    bool DeletePoll(int id);
}

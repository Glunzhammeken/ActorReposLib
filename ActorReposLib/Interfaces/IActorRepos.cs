namespace ActorReposLib.Interfaces
{
    public interface IActorRepos
    {
        Actor Add(Actor actor);
        Actor? GetActorById(int id);
        IEnumerable<Actor> GetActors(int? Birthyearbefore = null, int? Birthyearafter = null, string? name = null, string? sortBy = null);
        Actor Remove(int id);
        Actor? UpdateActor(int id, Actor nyData);
    }
}
using System;
using System.Linq;
using ProfileLib.RepoProfile;

namespace ProfileLib.RepoGameService
{
    public class GameService : IGameService
    {
        private readonly ProfileRepoDBContext _context;

        public GameService(ProfileRepoDBContext context)
        {
            _context = context;
        }

        public CurrentUser? StartGame(int profileId)
        {
            var profile = _context.Profiles.Find(profileId);
            if (profile == null)
            {
                return null;
            }

            var currentUser = _context.CurrentUsers.FirstOrDefault();

            if (currentUser == null)
            {
                currentUser = new CurrentUser
                {
                    ProfileId = profileId,
                    IsPlaying = true,
                    GameStartedAt = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow
                };
                _context.CurrentUsers.Add(currentUser);
            }
            else
            {
                currentUser.ProfileId = profileId;
                currentUser.IsPlaying = true;
                currentUser.GameStartedAt = DateTime.UtcNow;
                currentUser.LastUpdated = DateTime.UtcNow;
            }

            _context.SaveChanges();
            return currentUser;
        }

        public CurrentUser? EndGame()
        {
            var currentUser = _context.CurrentUsers.FirstOrDefault();
            if (currentUser == null)
            {
                return null;
            }

            currentUser.IsPlaying = false;
            currentUser.LastUpdated = DateTime.UtcNow;
            _context.SaveChanges();

            return currentUser;
        }

        public CurrentUser? GetCurrentGame()
        {
            return _context.CurrentUsers
                .Where(cu => cu.IsPlaying)
                .FirstOrDefault();
        }

        public Profile? AddScore(int points)
        {
            var currentUser = GetCurrentGame();
            if (currentUser == null)
            {
                return null;
            }

            var profile = _context.Profiles.Find(currentUser.ProfileId);
            if (profile == null)
            {
                return null;
            }

            profile.Score += points;
            currentUser.LastUpdated = DateTime.UtcNow;
            _context.SaveChanges();

            return profile;
        }
    }
}
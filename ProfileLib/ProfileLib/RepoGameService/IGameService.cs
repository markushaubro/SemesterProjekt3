using ProfileLib.RepoProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileLib.RepoGameService
{
    public interface IGameService
    {
        CurrentUser? StartGame(int profileId);
        CurrentUser? EndGame();
        CurrentUser? GetCurrentGame();
        Profile? AddScore(int points);
    }
}

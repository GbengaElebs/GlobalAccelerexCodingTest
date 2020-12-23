using System.Collections.Generic;
using System.Linq;
using DataAccessor.GlobalAccelerex;
using DbModels.GlobalAccelerex;
using Newtonsoft.Json;

namespace Data
{
    public class Seed
    {
        public static void SeedData(GlobalAccelerexDataContext context)
        {
            if (!context.LocationData.Any())
            {
                var locationData = System.IO.File.ReadAllText("Data/LocationSeed.json");
                var locations = JsonConvert.DeserializeObject<List<LocationData>>(locationData);
                foreach (var location in locations)
                {
                    context.LocationData.Add(location);
                }
                context.SaveChanges();
            }
            if (!context.CharacterData.Any())
            {
                var characterData = System.IO.File.ReadAllText("Data/CharacterSeed.json");
                var characters = JsonConvert.DeserializeObject<List<CharacterData>>(characterData);
                foreach (var character in characters)
                {
                    context.CharacterData.Add(character);
                }
                context.SaveChanges();
            }
            if (!context.EpisodeData.Any())
            {
                var episodeData = System.IO.File.ReadAllText("Data/EpisodeSeed.json");
                var episodes = JsonConvert.DeserializeObject<List<EpisodeData>>(episodeData);
                foreach (var episode in episodes)
                {
                    context.EpisodeData.Add(episode);
                }
                context.SaveChanges();
            }
            if (!context.CommentsData.Any())
            {
                var commentsData = System.IO.File.ReadAllText("Data/CommentSeed.json");
                var comments = JsonConvert.DeserializeObject<List<CommentsData>>(commentsData);
                foreach (var comment in comments)
                {
                    context.CommentsData.Add(comment);
                }
                context.SaveChanges();
            }
            if (!context.EpisodeCharacter.Any())
            {
                var episodeCharData = System.IO.File.ReadAllText("Data/EpisodeCharacterSeed.json");
                var episodeChar = JsonConvert.DeserializeObject<List<EpisodeCharacter>>(episodeCharData);
                foreach (var episodeCha in episodeChar)
                {
                    context.EpisodeCharacter.Add(episodeCha);
                }
                context.SaveChanges();
            }

        }
    }
}
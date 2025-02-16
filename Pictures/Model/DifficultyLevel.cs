namespace Pictures.Model
{
    /// <summary>
    /// Enum z nazwami klas strategii doboru kart.
    /// </summary>
    public enum DifficultyLevelName
    {
        Easy,
        Medium,
        Hard
    }

    /// <summary>
    /// Klasa abstrakcyjna reprezentująca grupę strategii doboru kart.
    /// </summary>
    public abstract class DifficultyLevel
    {
        public DifficultyLevelName Name { get; protected set; }
    }

    /// <summary>
    /// Preferowany jest dobór kart, aby różnorodność tagów była jak największa.
    /// </summary>
    public class EasyLevel : DifficultyLevel
    {
        public EasyLevel()
        {
            Name = DifficultyLevelName.Easy;
        }
    }

    /// <summary>
    /// Domyślny zupełnie losowy i nieskorelowany dobór kart.
    /// </summary>
    public class MediumLevel : DifficultyLevel
    {
        public MediumLevel()
        {
            Name = DifficultyLevelName.Medium;
        }
    }

    /// <summary>
    /// Preferowany jest dobór kart, aby tagi się powtarzały.
    /// </summary>
    public class HardLevel : DifficultyLevel
    {
        public HardLevel()
        {
            Name = DifficultyLevelName.Hard;
        }
    }
}

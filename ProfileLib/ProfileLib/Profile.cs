using System.ComponentModel.DataAnnotations;

namespace ProfileLib
{
    public class Profile
    {
		[Key]
		public int ID { get; set; }
		private string? _name;

		public string? Name
		{
			get { return _name; }
			set 
			{
				if (value is null)
				{
					throw new ArgumentNullException("The name can't be null");
				}
				if (value.Length <= 3)
				{
					throw new ArgumentOutOfRangeException(nameof(value), "The name must be longer than 3 chars");
				}
				_name = value;
			}
		}

		private int _score;

		public int Score
		{
			get 
			{ 
				return _score; 
			}
			set 
			{ 
				if(value < 0)
				{
					throw new ArgumentOutOfRangeException("Score can only be positive");
				}
				_score = value; 
			}
		}
		public int markus { get; set; }

	}
}

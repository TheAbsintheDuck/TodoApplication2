using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoWebApi.Tests
{
	public class NoteProcessorTest
	{
		private NoteProcessor _processor;
		public NoteProcessorTest()
		{
			_processor = new NoteProcessor();
		}

		[Fact]
		public void Should_Post_One_Note()
		{
			//ARRANGE
			var post = new Note
			{
				Text = "Shop",
				IsDone = false
			};

			//ACT
			_processor.Post(post);

			List<Note> postNote = _processor.GetAllNotes();

			//ASSERT
			Assert.NotNull(postNote);
			Assert.Single(postNote);
		}
	}
}

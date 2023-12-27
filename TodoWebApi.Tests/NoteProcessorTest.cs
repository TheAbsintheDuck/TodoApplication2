using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoWebApi.Models;
using TodoWebApi.Processors;

namespace TodoWebApi.Tests
{
	public class NoteProcessorTest
	{
		private NoteProcessor _processor;
		public NoteProcessorTest()
		{
			_processor = new NoteProcessor();
			NoteProcessor.notes.Clear();
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

		[Fact]
		public void Should_Get_List_Of_Notes()
		{
			//ARRANGE
			var post1 = new Note
			{
				Text = "Shop",
				IsDone = false
			};

			var post2 = new Note
			{
				Text = "Study",
				IsDone = false
			};

			var post3 = new Note
			{
				Text = "Clean house",
				IsDone = false
			};

			//ACT
			_processor.Post(post1);
			_processor.Post(post2);
			_processor.Post(post3);

			List<Note> getNotes = _processor.GetAllNotes();

			//ASSERT
			Assert.NotNull(getNotes);
			Assert.Equal(3, getNotes.Count);
		}

		[Fact]
		public void Should_Get_Empty_List()
		{
			//ACT
			List<Note> getNotes = _processor.GetAllNotes();

			//ASSERT
			Assert.Empty(getNotes);
		}

		[Fact]
		public void Should_Delete_Note()
		{
			//ARRANGE
			var post1 = new Note
			{
				Text = "Shop",
				IsDone = false
			};

			var post2 = new Note
			{
				Text = "Study",
				IsDone = false
			};

			var post3 = new Note
			{
				Text = "Clean house",
				IsDone = false
			};

			//ACT
			_processor.Post(post1);
			_processor.Post(post2);
			_processor.Post(post3);
			_processor.Delete(post2.Id);

			List<Note> getNotesAfterDelete = _processor.GetAllNotes();

			//ASSERT
			Assert.DoesNotContain(post2, getNotesAfterDelete);
		}

		[Fact]
		public void Should_Mark_Note_As_Done()
		{
			//ARRANGE
			var post = new Note
			{
				Text = "Shop",
				IsDone = false
			};

			//ACT
			_processor.Post(post);
			_processor.Check(post);

			var getNote = _processor.GetAllNotes().FirstOrDefault(n => n.Id == post.Id);

			//ASSERT
			Assert.NotNull(getNote);
			Assert.True(getNote.IsDone);
		}
	}
}

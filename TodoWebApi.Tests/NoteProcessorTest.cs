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
		//An instance of NoteProcessor for executing tests.
		private NoteProcessor _processor;

		//A constructor that creates a new instance of NoteProcessor and clears the list with notes for every test.
		public NoteProcessorTest()
		{
			_processor = new NoteProcessor();
			NoteProcessor.notes.Clear();
		}

		//Test to ensure that a note is added to the list
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

		//Test to ensure that a list with notes is retrieved.
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

		//Test to ensure that an empty list is retrieved.
		[Fact]
		public void Should_Get_Empty_List()
		{
			//ACT
			List<Note> getNotes = _processor.GetAllNotes();

			//ASSERT
			Assert.Empty(getNotes);
		}

		//Test to ensure that a note can be deleted from the list.
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

		//Test to ensure that a note can be marked as done.
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

		//Test to ensure that the number of remaining notes is counted.
		[Fact]
		public void Should_Show_Remaining_Notes()
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

			//ACT
			_processor.Post(post1);
			_processor.Post(post2);

			var remaining = _processor.CountNotes();

			//ASSERT
			Assert.NotNull(remaining);
			Assert.Equal("2", remaining);
		}
	}
}

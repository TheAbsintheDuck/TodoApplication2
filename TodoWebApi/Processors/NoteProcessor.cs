using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoWebApi.Models;

namespace TodoWebApi.Processors
{
	//HTTP calls for handling notes.
	[ApiController]
	[Route("/notes")]
	public class NoteProcessor
	{
		//List for storing notes and a counter for increasing the ID for each note.
		public static List<Note> notes = new List<Note>();
		public static int counter = 1;

		//Method for check or uncheck note as done with the ID that matches the note in the list
		[HttpPut("{id:int}")]
		public void Check(Note post)
		{
			var checkbox = notes.FirstOrDefault(n => n.Id == post.Id);

			//If the note exists, IsDone is set to true. If the note is already set to true it sets it to false
			if (checkbox != null)
			{
				checkbox.IsDone = post.IsDone;

				if (checkbox.IsDone == false)
				{
					post.IsDone = true;
				}

				else
				{
					post.IsDone = false;
				}
			}
		}

		//Method for counting all notes that is not set to done
		[HttpGet("/remaining")]
		public string CountNotes()
		{
			var remaining = notes.Count(n => !n.IsDone);
			return remaining.ToString();
		}

		//Method for deleting a note with the ID the matches the note in the list
		[HttpDelete("{id:int}")]
		public void Delete(int id)
		{
			var note = notes.FirstOrDefault(n => n.Id == id);

			if (note != null)
			{
				notes.Remove(note);
			}
		}

		//Method for returning all notes in the list.
		[HttpGet]
		public List<Note> GetAllNotes()
		{
			return notes;
		}

		//Method for adding a new post to the list and throw an exception if the note is null.
		[HttpPost]
		public void Post(Note post)
		{
			if (post == null)
			{
				throw new ArgumentNullException(nameof(post));
			}

			post.Id = counter++;
			notes.Add(post);
		}
	}
}

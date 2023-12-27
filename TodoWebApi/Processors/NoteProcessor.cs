﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoWebApi.Models;

namespace TodoWebApi.Processors
{
	[ApiController]
	[Route("/notes")]
	public class NoteProcessor
	{
		public static List<Note> notes = new List<Note>();
		public static int counter = 1;

		[HttpPut("{id:int}")]
		public void Check(Note post)
		{
			var checkbox = notes.FirstOrDefault(n => n.Id == post.Id);

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

		[HttpGet("/remaining")]
		public string CountNotes()
		{
			var remaining = notes.Count(n => !n.IsDone);
			return remaining.ToString();
		}

		[HttpDelete("{id:int}")]
		public void Delete(int id)
		{
			var note = notes.FirstOrDefault(n => n.Id == id);

			if (note != null)
			{
				notes.Remove(note);
			}
		}

		[HttpGet]
		public List<Note> GetAllNotes()
		{
			return notes;
		}

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

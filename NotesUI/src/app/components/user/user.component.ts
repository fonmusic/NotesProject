import { Component, OnInit } from '@angular/core';
import { Note } from 'src/app/models/note';
import { ServiceResponse } from 'src/app/models/serviceResponse';
import { NoteService } from 'src/app/services/note.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  notes: Note[] = [];
  newNote: Note = { title: '', text: '', createdDate: new Date(), updatedDate: new Date() };

  newNoteTitle: string = '';
  newNoteText: string = '';

  showCreateNoteForm: boolean = false;
  showNoteText: boolean[] = [];
  editNoteIndex: number = -1;

  constructor(private noteService: NoteService) { }

  ngOnInit(): void {
    this.getNotes();
  }

  getNotes(): void {
    this.noteService.getAllNotes().subscribe(
      (response: ServiceResponse<Note[]>) => { this.notes = response.data })
  }

  addNote(): void {
    this.noteService.addNote(this.newNote).subscribe(
      (response: ServiceResponse<Note>) => {
        this.notes.push(response.data);
        this.newNote = { title: '', text: '', createdDate: new Date(), updatedDate: new Date() };
      }
    )
  }

  updateNote(note: Note): void {
    const updatedNote: Note = {
      ...note,
      title: note.title,
      text: note.text,
      updatedDate: new Date()
    };
    this.noteService.updateNote(note.id!, updatedNote).subscribe(
      (response: ServiceResponse<Note>) => {
        const index = this.notes.findIndex(n => n.id === note.id);
        if (index !== -1) {
          this.notes[index] = response.data;
          this.newNote = { title: '', text: '', createdDate: new Date(), updatedDate: new Date() };
        }
      });
  }

  deleteNoteById(id: number | undefined): void {
    if (id) {
      this.noteService.deleteNoteById(id).subscribe(
        (response: ServiceResponse<boolean>) => {
          this.notes = this.notes.filter(n => n.id !== id);
        }
      );
    }
  }
  

  toggleCreateNoteForm(): void {
    this.showCreateNoteForm = !this.showCreateNoteForm;
  }
  
  toggleNoteText(index: number): void {
    this.showNoteText[index] = !this.showNoteText[index];
  }
  
  toggleEditNoteForm(index: number): void {
    this.editNoteIndex = index;
  }
  
  closeEditNoteForm(): void {
    this.editNoteIndex = -1;
  }

}
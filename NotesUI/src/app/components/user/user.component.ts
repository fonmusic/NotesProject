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
  newNoteTitle = '';

  showCreateNoteForm: boolean = false;
  showNoteText: boolean[] = [];
  editNoteIndex: number = -1;
  notesNotFound: boolean = false;
  buttonNoteEditorLabel: string = 'Crate a new note';

  successMessage: string = '';
  errorMessage: string = '';

  constructor(private noteService: NoteService) { }

  ngOnInit(): void {
    this.getNotes();
  }

  getNotes(): void {
    this.noteService.getAllNotes().subscribe(
      (response: ServiceResponse<Note[]>) => {
        this.notes = response.data;
        this.notesNotFound = false;
      },
      (error: any) => {
        this.notesNotFound = true;
      }
    );
  }

  getNoteByWords(words: string): void {
    if (words !== '') {
      this.noteService.getNoteByWords(words).subscribe(
        (response: ServiceResponse<Note[]>) => {
          this.notes = response.data;
          this.notesNotFound = false;
        },
        (error: any) => {
          this.notesNotFound = true;
        }
      );
    }
  }

  addNote(): void {
    if (this.newNoteTitle.trim() === '') {
      this.errorMessage = 'Adding an empty note is prohibited.';
      this.clearMessagesAfterDelay();
      return;
    }
    this.noteService.addNote({
      title: this.newNoteTitle,
      text: this.newNote.text,
      createdDate: new Date(),
      updatedDate: new Date()
    }).subscribe(
      (response: ServiceResponse<Note>) => {
        this.notes.push(response.data);
        this.newNoteTitle = '';
        this.newNote.text = '';
        this.successMessage = 'Note added successfully.';
        this.clearMessagesAfterDelay();
      },
      (error: any) => {
        this.errorMessage = 'Failed to add note.';
        this.clearMessagesAfterDelay();
        this.toggleCreateNoteForm();
      }
    );
  }


  updateNote(note: Note): void {
    const updatedNote: Note = {
      ...note,
      updatedDate: new Date()
    };
    this.noteService.updateNote(note.id!, updatedNote).subscribe(
      (response: ServiceResponse<Note>) => {
        const index = this.notes.findIndex(n => n.id === note.id);
        if (index !== -1) {
          this.notes[index] = response.data;
          this.newNote = { title: '', text: '', createdDate: new Date(), updatedDate: new Date() };
          this.successMessage = 'Note updated successfully.';
          this.clearMessagesAfterDelay();
          this.closeEditNoteForm(); 
        }
      },
      (error: any) => {
        this.errorMessage = 'Failed to update note.';
        this.clearMessagesAfterDelay();
      });
  }

  deleteNoteById(id: number | undefined): void {
    if (id) {
      this.noteService.deleteNoteById(id).subscribe(
        (response: ServiceResponse<boolean>) => {
          this.notes = this.notes.filter(n => n.id !== id);
          this.successMessage = 'Note deleted successfully.';
          this.clearMessagesAfterDelay();
        },
        (error: any) => {
          this.errorMessage = 'Failed to delete note.';
          this.clearMessagesAfterDelay();
          this.closeEditNoteForm();
        }
      );
    }
  }

  toggleCreateNoteForm(): void {
    if (this.showCreateNoteForm) {
      this.showCreateNoteForm = false;
    } else {
      for (let i = 0; i < this.showNoteText.length; i++) {
        this.closeNoteText(i);
      }
      this.showCreateNoteForm = true;
    }
    this.buttonNoteEditorLabel = this.showCreateNoteForm ? 'Close note editor' : 'Crate a new note';
  }

  toggleNoteText(index: number): void {
    this.showCreateNoteForm = false;
    this.closeEditNoteForm();
    this.showNoteText[index] = !this.showNoteText[index];
  }

  closeNoteText(index: number): void {
    this.showNoteText[index] = false;
  }

  toggleEditNoteForm(index: number): void {
    this.editNoteIndex = index;
  }

  closeEditNoteForm(): void {
    this.editNoteIndex = -1;
  }

  resetSearch(): void {
    this.getNotes();
  }

  clearSearch(): void {
    this.newNote.title = '';
    this.resetSearch();
  }

  clearMessagesAfterDelay(): void {
    setTimeout(() => {
      this.successMessage = '';
      this.errorMessage = '';
    }, 3000);
  }

}

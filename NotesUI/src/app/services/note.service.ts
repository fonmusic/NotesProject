import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ServiceResponse } from '../models/serviceResponse';
import { Note } from '../models/note';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NoteService {
  private apiUrl = `${environment.apiUrl}/Notes`;

  constructor(private http: HttpClient) { }

  getAllNotes(): Observable<ServiceResponse<Note[]>> {
    return this.http.get<ServiceResponse<Note[]>>(`${this.apiUrl}/GetAllNotes`);
  }

  getNoteByWords(words: string): Observable<ServiceResponse<Note[]>> {
    return this.http.get<ServiceResponse<Note[]>>(`${this.apiUrl}/GetNoteByWords/${words}`);
  }

  addNote(newNote: Note): Observable<ServiceResponse<Note>> {
    return this.http.post<ServiceResponse<Note>>(`${this.apiUrl}/CreateNote`, newNote);
  }

  updateNote(id: number, updatedNote: Note): Observable<ServiceResponse<Note>> {
    return this.http.put<ServiceResponse<Note>>(`${this.apiUrl}/EditNote/${id}`, updatedNote);
  }

  deleteNoteById(id: number): Observable<ServiceResponse<boolean>> {
    return this.http.delete<ServiceResponse<boolean>>(`${this.apiUrl}/DeleteNoteById/${id}`);
  }
}

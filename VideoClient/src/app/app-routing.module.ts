import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DocumentListComponent } from './components/document-list/document-list.component';
import { WatchComponent } from './components/watch/watch.component';

const routes: Routes = [
  { path: '', component: DocumentListComponent },
  { path: 'watch/:id', component: WatchComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

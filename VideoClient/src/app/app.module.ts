import { HttpClientModule } from "@angular/common/http";
import { NgModule } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WatchComponent } from './components/watch/watch.component';
import { AmpPlayerComponent } from './components/amp-player/amp-player.component';
import { DocumentListComponent } from './components/document-list/document-list.component';


@NgModule({
  declarations: [
    AppComponent,
    WatchComponent,
    AmpPlayerComponent,
    DocumentListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

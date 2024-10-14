import { Component, Renderer2, OnInit } from '@angular/core';
import {
  faInfoCircle,
  faMoon,
  faUser,
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  currentTheme = 'dark-theme';
  faUser = faUser;
  faInfoCircle = faInfoCircle;
  faMoon = faMoon;

  constructor(private renderer: Renderer2) {}

  ngOnInit(): void {
    this.loadTheme();
  }

  toggleTheme(): void {
    const newTheme =
      this.currentTheme === 'dark-theme' ? 'light-theme' : 'dark-theme';
    this.applyTheme(newTheme);
  }

  private loadTheme(): void {
    const savedTheme = localStorage.getItem('theme') || this.currentTheme;
    this.applyTheme(savedTheme);
  }

  private applyTheme(theme: string): void {
    this.renderer.removeClass(document.body, this.currentTheme);
    this.currentTheme = theme;
    this.renderer.addClass(document.body, this.currentTheme);
    localStorage.setItem('theme', this.currentTheme);
  }
}

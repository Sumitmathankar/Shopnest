import { CommonModule } from '@angular/common';
import { Component, OnInit,inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-productpage',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './productpage.component.html',
  styleUrl: './productpage.component.scss',
})
export class ProductpageComponent implements OnInit {
 private http = inject(HttpClient);
  private route = inject(ActivatedRoute);

  // 1. Define missing properties
  currentCategory: string = '';
  activeSubFilter: string = 'all';
  loading: boolean = false;
  
  allCategoryProducts: any[] = []; // Master copy for the category
  filteredProducts: any[] = [];    // What actually shows on screen

  ngOnInit() {
    // Listen for URL changes (e.g., /products/fashion to /products/electronics)
    this.route.paramMap.subscribe(params => {
      this.currentCategory = params.get('category') || 'all';
      this.activeSubFilter = 'all'; // Reset sub-filter on category change
      this.loadDataFromApi(this.currentCategory);
    });
  }

  // 2. Consolidated API call
  loadDataFromApi(category: string) {
    this.loading = true;
    
    // Using your API endpoint - make sure the URL is correct
    const url = 'https://localhost:7297/products/Products'; 
    let params = new HttpParams();

    if (category !== 'all') {
      params = params.set('category', category);
    }

    this.http.get<any[]>(url, { params }).subscribe({
      next: (data) => {
        this.allCategoryProducts = data;
        this.filteredProducts = data; // Initially show everything in category
        this.loading = false;
      },
      error: (err) => {
        console.error('Fetch error:', err);
        this.loading = false;
      }
    });
  }

  // 3. Gender filtering logic
  filterByGender(gender: string) {
    this.activeSubFilter = gender;
    if (gender === 'all') {
      this.filteredProducts = this.allCategoryProducts;
    } else {
      // Filters from the local master copy to avoid unnecessary API hits
      this.filteredProducts = this.allCategoryProducts.filter(
        p => p.gender?.toLowerCase() === gender.toLowerCase()
      );
    }
  }

  // Placeholder methods for buttons
  addToCart(product: any) {
    console.log('Added to cart:', product);
  }

  buyNow(product: any) {
    console.log('Buying now:', product);
  }
}

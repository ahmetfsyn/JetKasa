# Jet Kasa

Jet Kasa, işletmeler için **kasa ve POS işlemlerini** yönetmeyi kolaylaştıran bir web uygulamadır. Ürün ekleme, sepet yönetimi ve ödeme işlemleri hızlı ve güvenli bir şekilde yapılabilir.

---

## Özellikler

- Ürünleri barkodla veya manuel ekleme  
- Sepet Active; tamamlandığında Completed, iptal edilirse Cancelled  
- Ödeme sadece kart ile yapılır ve sepet Completed olur  
- SignalR ile anlık ödeme bildirimi  
- Novu ile e-postaya fiş gönderimi  

---

## Kullanılan Teknolojiler

### Backend
- .NET 9 Web API  
- Entity Framework Core (PostgreSQL)  
- MediatR, Mapster, FluentValidation  
- TS.Result, TS.EntityFrameworkCore.GenericRepository  
- Scalar.AspNetCore (performans ve izleme)  
- Novu (bildirim ve e-posta)  
- SignalR (gerçek zamanlı güncellemeler)  

### Frontend
- React 19 + Vite 7  
- TypeScript 5  
- TailwindCSS 4 + @tailwindcss/vite  
- React Hook Form & Yup  
- Zustand (state management)  
- Axios, clsx, tailwind-merge  
- Lucide React (ikonlar)  
- React Router 7  
- React Toastify  

### Mobil (POS)
- React Native 0.80  
- React Navigation  
- React Native Paper (UI)  
- React Native NFC Manager  
- SignalR (gerçek zamanlı ödeme)  
- Axios  

---

## Kurulum


Depoyu klonlayın:
   ```bash
git clone https://github.com/kullaniciadi/jet-kasa.git


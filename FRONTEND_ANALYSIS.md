# SpendWise Frontend Analysis

> Angular 19 frontend documentation — prepared for Next.js migration

---

## Technology Stack

| Category | Technology | Version |
|---|---|---|
| Framework | Angular | 19.2.1 |
| Language | TypeScript | 5.8.2 |
| Reactive | RxJS | 7.8.2 |
| UI Components | PrimeNG | 19.0.9 |
| Dashboard Template | Admin LTE | 3.2.0 |
| Bootstrap Modals/Tabs | ngx-bootstrap | 19.0.2 |
| Pagination | ngx-pagination | 6.0.3 |
| Icons | Font Awesome | 6.7.2 |
| Alerts | SweetAlert2 | 11.17.2 |
| Real-time | SignalR | 8.0.7 |
| Date Handling | Moment.js + Timezone | 2.30.1 |
| Utilities | Lodash-es | 4.17.21 |
| Boilerplate | ABP Framework | 12.1.0 |
| Code Gen | NSwag | 14.2.0 |

---

## Project Structure

```
angular/
├── src/
│   ├── account/               # Auth module (login, register)
│   ├── app/                   # Main app module
│   │   ├── layout/            # Header, Sidebar, Footer components
│   │   ├── home/              # Dashboard
│   │   ├── about/             # About page
│   │   ├── users/             # User management + modals
│   │   ├── roles/             # Role management + modals
│   │   └── tenants/           # Tenant management + modals
│   ├── shared/                # Services, pipes, base classes, components
│   │   ├── auth/              # Auth service, route guard
│   │   ├── session/           # AppSessionService
│   │   ├── layout/            # LayoutStoreService
│   │   ├── service-proxies/   # Auto-generated API clients
│   │   └── components/        # Reusable modal/pagination/validation
│   ├── assets/
│   │   ├── appconfig.json     # Runtime config (API base URL)
│   │   └── freeze-ui/         # Loading overlay CSS
│   ├── environments/          # env.ts, env.prod.ts
│   ├── app-initializer.ts     # Bootstrap logic
│   ├── root.component.ts
│   ├── root.module.ts
│   └── root-routing.module.ts
├── angular.json
└── package.json
```

---

## Routing

### Root Routes

```
''        → redirect to /app/about
account/  → AccountModule (lazy)
app/      → AppModule (lazy)
```

### Account Routes

| Path | Component | Guard |
|---|---|---|
| `account/login` | LoginComponent | public |
| `account/register` | RegisterComponent | public |

### App Routes

| Path | Component | Permission |
|---|---|---|
| `app/home` | HomeComponent | authenticated |
| `app/about` | AboutComponent | authenticated |
| `app/users` | UsersComponent | `Pages.Users` |
| `app/roles` | RolesComponent | `Pages.Roles` |
| `app/tenants` | TenantsComponent | `Pages.Tenants` |
| `app/update-password` | ChangePasswordComponent | authenticated |

---

## Layout Structure

```html
<div class="wrapper">
  <app-header>                        <!-- top navbar -->
    <header-left-navbar />            <!-- logo + sidebar toggle -->
    <header-language-menu />          <!-- language switcher -->
    <header-user-menu />              <!-- user dropdown: profile, logout -->
  </app-header>

  <sidebar class="main-sidebar">
    <sidebar-logo />
    <sidebar-user-panel />            <!-- avatar + username -->
    <sidebar-menu />                  <!-- permission-gated nav items -->
  </sidebar>

  <div class="content-wrapper">
    <router-outlet />                 <!-- page content renders here -->
  </div>

  <app-footer />
  <div id="sidebar-overlay" />       <!-- mobile overlay -->
</div>
```

---

## Component Architecture

### Base Classes

| Class | Used By | Provides |
|---|---|---|
| `AppComponentBase` | All components | localization, permissions, notifications, session |
| `PagedListingComponentBase<T>` | List page components | pagination, filtering, sorting |

### Layout Components

| Component | File | Purpose |
|---|---|---|
| HeaderComponent | `app/layout/header.component.ts` | Top navigation bar |
| SidebarComponent | `app/layout/sidebar.component.ts` | Left sidebar wrapper |
| SidebarMenuComponent | `app/layout/sidebar-menu.component.ts` | Dynamic permission-gated menu |
| HeaderUserMenuComponent | `app/layout/header-user-menu.component.ts` | User dropdown |
| HeaderLanguageMenuComponent | `app/layout/header-language-menu.component.ts` | Language switcher |
| FooterComponent | `app/layout/footer.component.ts` | Footer bar |

### Account Components

| Component | Purpose |
|---|---|
| AccountComponent | Layout wrapper for auth pages |
| LoginComponent | Login form |
| RegisterComponent | Registration form |
| TenantChangeComponent | Tenant switcher in login header |
| TenantChangeDialogComponent | Tenant selection modal |

### Feature Components (CRUD Pages)

Each feature follows this pattern:

```
FeatureComponent (extends PagedListingComponentBase)
├── Search input + filter panel (collapsible)
├── PrimeNG DataTable (server-side lazy loading)
│   └── Row actions: Edit | Delete | (Reset Password for users)
├── CreateFeatureDialogComponent (ngx-bootstrap Modal)
│   ├── AbpModalHeaderComponent
│   ├── Tabs: Details | Roles/Permissions
│   └── AbpModalFooterComponent
└── EditFeatureDialogComponent (same structure)
```

| Page | Components |
|---|---|
| Users | UsersComponent, CreateUserDialogComponent, EditUserDialogComponent, ResetPasswordComponent |
| Roles | RolesComponent, CreateRoleDialogComponent, EditRoleDialogComponent |
| Tenants | TenantsComponent, CreateTenantDialogComponent, EditTenantDialogComponent |
| Change Password | ChangePasswordComponent (standalone page, not modal) |

### Shared Reusable Components

| Component | Purpose |
|---|---|
| `AbpModalHeaderComponent` | Consistent modal header with title + close button |
| `AbpModalFooterComponent` | Cancel / Save buttons with loading state |
| `AbpValidationSummaryComponent` | Displays server-side validation errors |
| `AbpPaginationControlsComponent` | Pagination controls wrapping ngx-pagination |

---

## State Management

No Redux/NgRx. State is managed via:

1. **AppSessionService** — current user, tenant, application info
   - Initialized on app bootstrap
   - Properties: `user`, `tenant`, `application`, `userId`, `tenantId`
   - Method: `changeTenantIfNeeded()`

2. **LayoutStoreService** — sidebar expanded/collapsed
   - Observable: `sidebarExpanded$`
   - Method: `setSidebarExpanded(value: boolean)`

3. **Service Proxies** — all data fetching returns Observables; components subscribe directly

---

## API Integration

### Configuration

- Base URL stored in `src/assets/appconfig.json`:
  ```json
  {
    "remoteServiceBaseUrl": "https://localhost:44311",
    "appBaseUrl": "http://localhost:4200"
  }
  ```

### HTTP Interceptor

`AbpHttpInterceptor` automatically attaches:

```
Authorization: Bearer {accessToken}
Abp.TenantId: {tenantId}
.AspNetCore.Culture: c={lang}|uic={lang}
Content-Type: application/json
```

### Service Proxies (Auto-generated by NSwag)

All proxies live in `shared/service-proxies/service-proxies.ts`.

**AccountServiceProxy**
```typescript
isTenantAvailable(input: IsTenantAvailableInput): Observable<IsTenantAvailableOutput>
register(input: RegisterInput): Observable<RegisterOutput>
```

**TokenAuthServiceProxy**
```typescript
authenticate(input: AuthenticateModel): Observable<AuthenticateResultModel>
```

**SessionServiceProxy**
```typescript
getCurrentLoginInformations(): Observable<GetCurrentLoginInformationsOutput>
```

**UserServiceProxy**
```typescript
getAll(keyword, isActive, sorting, skipCount, maxResultCount): Observable<UserDtoPagedResultDto>
get(id): Observable<UserDto>
create(input: CreateUserDto): Observable<void>
update(input: UserDto): Observable<void>
delete(id): Observable<void>
getRoles(): Observable<RoleDtoListResultDto>
changePassword(input: ChangePasswordDto): Observable<boolean>
```

**RoleServiceProxy**
```typescript
getAll(keyword, sorting, skipCount, maxResultCount): Observable<RoleDtoPagedResultDto>
get(id): Observable<RoleDto>
create(input: CreateRoleDto): Observable<void>
update(input: RoleDto): Observable<void>
delete(id): Observable<void>
getAllPermissions(): Observable<PermissionDtoListResultDto>
```

**TenantServiceProxy**
```typescript
getAll(keyword, isActive, sorting, skipCount, maxResultCount): Observable<TenantDtoPagedResultDto>
create(input: CreateTenantDto): Observable<void>
update(input: TenantDto): Observable<void>
delete(id): Observable<void>
```

### Data Shapes

```typescript
interface PagedResult<T> {
  items: T[];
  totalCount: number;
}

interface UserDto {
  id: number;
  userName: string;
  name: string;
  surname: string;
  fullName: string;
  emailAddress: string;
  isActive: boolean;
  roleNames: string[];
}

interface RoleDto {
  id: number;
  displayName: string;
  normalizedName: string;
  description: string;
}

interface PermissionDto {
  name: string;
  displayName: string;
  description: string;
  isGranted: boolean;
}

interface TenantDto {
  id: number;
  name: string;
  tenancyName: string;
  isActive: boolean;
  connectionString: string;
}

interface AuthenticateModel {
  userNameOrEmailAddress: string;
  password: string;
  rememberClient?: boolean;
}

interface AuthenticateResultModel {
  accessToken: string;
  encryptedAccessToken: string;
  expireInSeconds: number;
}
```

---

## Authentication & Authorization

### Login Flow

1. App init → load `appconfig.json`
2. Call `/AbpUserConfiguration/GetAll` (localization, settings, permissions)
3. Call `SessionService.getCurrentLoginInformations()` → populate `AppSessionService`
4. User submits login form → `TokenAuthServiceProxy.authenticate()`
5. Store `accessToken` in **localStorage**
6. Store `encryptedAccessToken` in **cookie** (`enc_auth_token`)
7. Redirect to home or originally requested URL

### Route Guard (`AppRouteGuard`)

```typescript
canActivate(route, state): boolean {
  if (!sessionService.user) {
    router.navigate(['/account/login']);
    return false;
  }
  if (route.data.permission && !permissionChecker.isGranted(route.data.permission)) {
    router.navigate([bestRoute]);
    return false;
  }
  return true;
}
```

### Permission Checking

- **Route level:** `data: { permission: 'Pages.Users' }`
- **Component level:** `this.isGranted('Pages.Users')`
- **Template level:** `*ngIf="isGranted('Pages.Users')"`
- **Menu level:** `isMenuItemVisible(item)` checks `item.permissionName`

### Logout Flow

1. Clear ABP auth token from localStorage
2. Clear `enc_auth_token` cookie
3. Redirect to app base URL or login page

---

## UI/UX Patterns

### Data Tables

- PrimeNG DataTable with server-side lazy loading
- Sortable columns
- Keyword search input
- Collapsible advanced filter panel
- Rows per page: 5, 10, 25, 50, 100, 250, 500 (default: 10)
- Row action buttons: Edit | Delete | (Reset Password)
- Confirmation dialog before delete (`abp.message.confirm`)

### Modals

- ngx-bootstrap `BsModalRef`
- Consistent structure: Header → Tabs → Footer
- Footer: Cancel button + Save button (disabled while saving)
- Validation summary above form fields for server errors
- Client-side validation: required, minlength, maxlength, email, pattern, custom `validateEqual`

### Notifications

| Type | Method |
|---|---|
| Success toast | `abp.notify.success(message)` |
| Warning dialog | `abp.message.warn(message)` |
| Error dialog | `abp.message.error(message)` |
| Confirm dialog | `abp.message.confirm(message, title, callback)` |
| Real-time push | SignalR → SweetAlert2 + Push.js desktop notification |

### Sidebar Menu Item Structure

```typescript
interface MenuItem {
  label: string;
  route?: string;
  icon?: string;          // Font Awesome class
  permissionName?: string;
  children?: MenuItem[];
  isExternalLink?: boolean;
}
```

---

## Styling

| Layer | Technology |
|---|---|
| Base template | Admin LTE 3.2.0 |
| Grid / utilities | Bootstrap 4 |
| Component theme | PrimeNG Lara theme |
| Icons | Font Awesome 6 |
| Custom styles | `src/shared/core.less` |
| Loading overlay | Freeze UI CSS |
| Flag icons | Famfamfam Flags |

### Key CSS Classes

```
.wrapper              Main layout wrapper
.main-header          Top navigation bar
.main-sidebar         Left sidebar
.content-wrapper      Page content area
.sidebar-mini         Compact sidebar mode
.sidebar-collapse     Collapsed sidebar
.login-page           Login/register page
.small-box            Dashboard stat card
.info-box             Information card
.card                 Generic card container
.hold-transition      Smooth sidebar transitions
```

### Responsive Behavior

- Sidebar collapses to icon-only on small screens
- `#sidebar-overlay` covers content on mobile when sidebar is open
- Tables scroll horizontally on small screens
- Modals become full-screen on mobile

---

## Key User Flows

### User Management

```
/app/users
  → Search by keyword, filter by isActive
  → Create User: modal → Details tab (name, surname, username, email, password) + Roles tab (checkboxes)
  → Edit User: same modal pre-populated
  → Reset Password: separate modal
  → Delete: confirm dialog → DELETE /api/services/app/User/Delete/{id}
```

### Role Management

```
/app/roles
  → Create Role: modal → Details tab (name, description) + Permissions tab (checkbox tree)
  → Edit Role: same modal
  → Delete: confirm dialog
```

### Tenant Management

```
/app/tenants  (Pages.Tenants permission required)
  → Create Tenant: modal (name, tenancyName, connection string)
  → Edit / Delete
  → Switch Tenant: header selector → sets Abp.TenantId cookie → page reload
```

### Change Password

```
/app/update-password
  → Form: current password, new password, confirm password
  → Validates: min 8 chars, uppercase, lowercase, number, passwords match
  → POST /api/services/app/User/ChangePassword
  → Success → redirect to home
```

### Localization

- Default source key: `'SpendWise'`
- Template usage: `{{ 'Users' | localize }}`
- Language change: sets `.AspNetCore.Culture` cookie → page reload
- Supported: en-US, pt-BR, zh-CN, he-IL (and more via ABP)

### Real-time Notifications (SignalR)

```
App init → SignalRAspNetCoreHelper.initSignalR()
         → WebSocket to /signalr?enc_auth_token={token}
         → abp.event.on('abp.notifications.received', handler)
         → Show SweetAlert2 dialog
         → Show Push.js desktop notification
         → Auto-dismiss after 6 seconds
```

---

## Next.js Migration Map

| Angular Concept | Next.js Equivalent |
|---|---|
| Lazy-loaded modules | App Router route groups `(auth)/`, `(app)/` |
| `AppRouteGuard` | Next.js middleware (`middleware.ts`) |
| `AppSessionService` | Zustand store or React Context |
| `LayoutStoreService` | Zustand store |
| Services + RxJS Observables | Custom hooks + React Query / SWR |
| Service Proxies (NSwag) | `openapi-typescript` generated types + `fetch` wrappers |
| ngx-bootstrap Modals | Radix UI Dialog / shadcn `<Dialog>` |
| PrimeNG DataTable | TanStack Table (react-table) |
| Admin LTE + Bootstrap | Tailwind CSS + shadcn/ui |
| `LocalizePipe` | `next-intl` |
| `ngIf="isGranted(...)"` | Custom `usePermission` hook |
| ABP auth cookies | `next-auth` or `iron-session` |
| SignalR client | `@microsoft/signalr` in a `useSignalR` hook |
| SweetAlert2 | `sonner` toasts + shadcn `<AlertDialog>` |

### Suggested Next.js App Router Structure

```
app/
├── (auth)/
│   ├── login/
│   │   └── page.tsx
│   └── register/
│       └── page.tsx
├── (app)/
│   ├── layout.tsx              ← sidebar + header shell
│   ├── home/
│   │   └── page.tsx
│   ├── about/
│   │   └── page.tsx
│   ├── users/
│   │   └── page.tsx
│   ├── roles/
│   │   └── page.tsx
│   ├── tenants/
│   │   └── page.tsx
│   └── update-password/
│       └── page.tsx
├── layout.tsx                  ← root layout (fonts, providers)
└── middleware.ts               ← auth + permission guard

components/
├── layout/
│   ├── Header.tsx
│   ├── Sidebar.tsx
│   ├── SidebarMenu.tsx
│   └── Footer.tsx
├── users/
│   ├── UsersTable.tsx
│   ├── CreateUserDialog.tsx
│   └── EditUserDialog.tsx
├── roles/
├── tenants/
└── shared/
    ├── DataTable.tsx
    ├── ConfirmDialog.tsx
    ├── PageHeader.tsx
    └── ValidationSummary.tsx

lib/
├── api/                        ← fetch wrappers for each service
│   ├── users.ts
│   ├── roles.ts
│   ├── tenants.ts
│   └── auth.ts
├── hooks/
│   ├── usePermission.ts
│   ├── useSession.ts
│   └── useSignalR.ts
└── types/                      ← shared TypeScript interfaces
    └── api.ts
```

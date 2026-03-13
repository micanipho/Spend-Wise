# Add API Service

Generate a typed API service file following the single-responsibility pattern: one file, one exported object with async CRUD methods, all calling a shared Axios instance.

## Arguments

`$ARGUMENTS` — the feature name (e.g. `invoice`, `lead`, `product`). If not provided, ask the user for it.

## Instructions

Parse `$ARGUMENTS` to extract:
- **featureName**: camelCase (e.g. `invoice`)
- **FeatureName**: PascalCase (e.g. `Invoice`)
- **featureNames**: plural camelCase (e.g. `invoices`)

Before generating, ask the user:
1. What is the base API endpoint path? (e.g. `/invoices`)
2. Which operations are needed? (getAll, getById, create, update, delete — and any custom ones like `getStats`, `approve`, `deactivate`)
3. What filters does the list endpoint support? (searchTerm, status, dateRange, etc.)
4. What TypeScript type represents this entity? (from `@/types` or define inline)
5. Does the list endpoint return `{ items: T[], totalCount: number }` or a plain array?

Then generate the file:

---

### File: `src/services/{featureName}Service.ts`

```typescript
import api from './api';
import { {FeatureName} } from '@/types';

export interface {FeatureName}Filters {
  pageNumber: number;
  pageSize: number;
  searchTerm?: string;
  // additional filters based on user input
}

export interface {FeatureName}ListResponse {
  items: {FeatureName}[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}

const {featureName}Service = {
  get{FeatureNames}: async (filters: {FeatureName}Filters): Promise<{FeatureName}ListResponse> => {
    try {
      const params = new URLSearchParams();
      params.append('pageNumber', String(filters.pageNumber));
      params.append('pageSize', String(filters.pageSize));
      if (filters.searchTerm) params.append('searchTerm', filters.searchTerm);
      // append additional filters

      const response = await api.get(`/{featureNames}?${params.toString()}`);
      return response.data;
    } catch (error: any) {
      throw new Error(error?.response?.data?.message ?? 'Failed to fetch {featureNames}');
    }
  },

  get{FeatureName}ById: async (id: string): Promise<{FeatureName}> => {
    try {
      const response = await api.get(`/{featureNames}/${id}`);
      return response.data;
    } catch (error: any) {
      throw new Error(error?.response?.data?.message ?? 'Failed to fetch {featureName}');
    }
  },

  create{FeatureName}: async (data: Partial<{FeatureName}>): Promise<{FeatureName}> => {
    try {
      const response = await api.post('/{featureNames}', data);
      return response.data;
    } catch (error: any) {
      throw new Error(error?.response?.data?.message ?? 'Failed to create {featureName}');
    }
  },

  update{FeatureName}: async (id: string, data: Partial<{FeatureName}>): Promise<{FeatureName}> => {
    try {
      const response = await api.put(`/{featureNames}/${id}`, data);
      return response.data;
    } catch (error: any) {
      throw new Error(error?.response?.data?.message ?? 'Failed to update {featureName}');
    }
  },

  delete{FeatureName}: async (id: string): Promise<void> => {
    try {
      await api.delete(`/{featureNames}/${id}`);
    } catch (error: any) {
      throw new Error(error?.response?.data?.message ?? 'Failed to delete {featureName}');
    }
  },

  // Add custom methods here based on user-specified operations
  // e.g. get{FeatureName}Stats, approve{FeatureName}, deactivate{FeatureName}
};

export default {featureName}Service;
```

---

After generating, remind the user to:
1. Add the `{FeatureName}` type to `src/types/index.ts` if it doesn't exist yet
2. Add any custom enums (e.g. `{FeatureName}Status`) to `src/types/enums.ts`
3. Run `/add-provider {featureName}` if state management is needed for this feature

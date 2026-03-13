# Add State Management Provider

Generate a complete 4-file state management provider for a new feature domain using the redux-actions + React Context + useReducer pattern.

## Arguments

`$ARGUMENTS` — the feature name (e.g. `invoice`, `lead`, `product`). If not provided, ask the user for it.

## Instructions

Parse `$ARGUMENTS` to extract:
- **featureName**: the raw name (e.g. `invoice`)
- **FeatureName**: PascalCase (e.g. `Invoice`)
- **featureNames**: plural camelCase (e.g. `invoices`)
- **FeatureNames**: plural PascalCase (e.g. `Invoices`)

Before generating any files, ask the user for the following if not obvious from context:
1. What state fields does this feature need? (e.g. `items`, `selectedItem`, `stats`, `filters`)
2. What actions are needed? (e.g. fetch list, fetch by id, create, update, delete, set filters)
3. What service file will this provider call? (e.g. `invoiceService`)
4. What is the main entity TypeScript type? (e.g. `Invoice` from `@/types`)

Then generate all 4 files:

---

### File 1: `src/providers/{featureName}Provider/context.tsx`

```typescript
import { createContext, useContext } from 'react';

export interface {FeatureName}Filters {
  pageNumber: number;
  pageSize: number;
  searchTerm?: string;
  // Add domain-specific filters based on user input
}

export interface {FeatureName}State {
  {featureNames}: {FeatureName}[];
  selected{FeatureName}: {FeatureName} | null;
  isPending: boolean;
  error: string | null;
  filters: {FeatureName}Filters;
  totalCount: number;
}

export interface {FeatureName}Actions {
  fetch{FeatureNames}: () => Promise<void>;
  fetch{FeatureName}ById: (id: string) => Promise<void>;
  create{FeatureName}: (data: Partial<{FeatureName}>) => Promise<void>;
  update{FeatureName}: (id: string, data: Partial<{FeatureName}>) => Promise<void>;
  delete{FeatureName}: (id: string) => Promise<void>;
  setFilters: (filters: Partial<{FeatureName}Filters>) => void;
  clearError: () => void;
}

export const INITIAL_STATE: {FeatureName}State = {
  {featureNames}: [],
  selected{FeatureName}: null,
  isPending: false,
  error: null,
  totalCount: 0,
  filters: {
    pageNumber: 1,
    pageSize: 10,
    searchTerm: '',
  },
};

export const {FeatureName}StateContext = createContext<{FeatureName}State>(INITIAL_STATE);
export const {FeatureName}ActionContext = createContext<{FeatureName}Actions | undefined>(undefined);

export const use{FeatureName} = () => useContext({FeatureName}StateContext);
export const use{FeatureName}Actions = () => {
  const context = useContext({FeatureName}ActionContext);
  if (!context) throw new Error('use{FeatureName}Actions must be used within {FeatureName}Provider');
  return context;
};
```

---

### File 2: `src/providers/{featureName}Provider/actions.tsx`

```typescript
import { createAction } from 'redux-actions';

export enum {FeatureName}ActionEnums {
  fetch{FeatureNames}Pending = 'FETCH_{FEATURENAMES}_PENDING',
  fetch{FeatureNames}Success = 'FETCH_{FEATURENAMES}_SUCCESS',
  fetch{FeatureNames}Error = 'FETCH_{FEATURENAMES}_ERROR',

  fetch{FeatureName}ByIdPending = 'FETCH_{FEATURENAME}_BY_ID_PENDING',
  fetch{FeatureName}ByIdSuccess = 'FETCH_{FEATURENAME}_BY_ID_SUCCESS',
  fetch{FeatureName}ByIdError = 'FETCH_{FEATURENAME}_BY_ID_ERROR',

  create{FeatureName}Pending = 'CREATE_{FEATURENAME}_PENDING',
  create{FeatureName}Success = 'CREATE_{FEATURENAME}_SUCCESS',
  create{FeatureName}Error = 'CREATE_{FEATURENAME}_ERROR',

  update{FeatureName}Pending = 'UPDATE_{FEATURENAME}_PENDING',
  update{FeatureName}Success = 'UPDATE_{FEATURENAME}_SUCCESS',
  update{FeatureName}Error = 'UPDATE_{FEATURENAME}_ERROR',

  delete{FeatureName}Pending = 'DELETE_{FEATURENAME}_PENDING',
  delete{FeatureName}Success = 'DELETE_{FEATURENAME}_SUCCESS',
  delete{FeatureName}Error = 'DELETE_{FEATURENAME}_ERROR',

  setFilters = 'SET_{FEATURENAME}_FILTERS',
  clearError = 'CLEAR_{FEATURENAME}_ERROR',
}

export const fetch{FeatureNames}Pending = createAction({FeatureName}ActionEnums.fetch{FeatureNames}Pending);
export const fetch{FeatureNames}Success = createAction<any>({FeatureName}ActionEnums.fetch{FeatureNames}Success);
export const fetch{FeatureNames}Error = createAction<string>({FeatureName}ActionEnums.fetch{FeatureNames}Error);

export const fetch{FeatureName}ByIdPending = createAction({FeatureName}ActionEnums.fetch{FeatureName}ByIdPending);
export const fetch{FeatureName}ByIdSuccess = createAction<any>({FeatureName}ActionEnums.fetch{FeatureName}ByIdSuccess);
export const fetch{FeatureName}ByIdError = createAction<string>({FeatureName}ActionEnums.fetch{FeatureName}ByIdError);

export const create{FeatureName}Pending = createAction({FeatureName}ActionEnums.create{FeatureName}Pending);
export const create{FeatureName}Success = createAction({FeatureName}ActionEnums.create{FeatureName}Success);
export const create{FeatureName}Error = createAction<string>({FeatureName}ActionEnums.create{FeatureName}Error);

export const update{FeatureName}Pending = createAction({FeatureName}ActionEnums.update{FeatureName}Pending);
export const update{FeatureName}Success = createAction({FeatureName}ActionEnums.update{FeatureName}Success);
export const update{FeatureName}Error = createAction<string>({FeatureName}ActionEnums.update{FeatureName}Error);

export const delete{FeatureName}Pending = createAction({FeatureName}ActionEnums.delete{FeatureName}Pending);
export const delete{FeatureName}Success = createAction({FeatureName}ActionEnums.delete{FeatureName}Success);
export const delete{FeatureName}Error = createAction<string>({FeatureName}ActionEnums.delete{FeatureName}Error);

export const setFiltersAction = createAction<any>({FeatureName}ActionEnums.setFilters);
export const clearErrorAction = createAction({FeatureName}ActionEnums.clearError);
```

---

### File 3: `src/providers/{featureName}Provider/reducer.tsx`

```typescript
import { handleActions } from 'redux-actions';
import { INITIAL_STATE, {FeatureName}State } from './context';
import { {FeatureName}ActionEnums } from './actions';

export const {featureName}Reducer = handleActions<{FeatureName}State, any>(
  {
    [{FeatureName}ActionEnums.fetch{FeatureNames}Pending]: (state) => ({
      ...state,
      isPending: true,
      error: null,
    }),
    [{FeatureName}ActionEnums.fetch{FeatureNames}Success]: (state, { payload }) => ({
      ...state,
      isPending: false,
      {featureNames}: payload.items ?? payload,
      totalCount: payload.totalCount ?? payload.length,
    }),
    [{FeatureName}ActionEnums.fetch{FeatureNames}Error]: (state, { payload }) => ({
      ...state,
      isPending: false,
      error: payload,
    }),

    [{FeatureName}ActionEnums.fetch{FeatureName}ByIdPending]: (state) => ({
      ...state,
      isPending: true,
      error: null,
    }),
    [{FeatureName}ActionEnums.fetch{FeatureName}ByIdSuccess]: (state, { payload }) => ({
      ...state,
      isPending: false,
      selected{FeatureName}: payload,
    }),
    [{FeatureName}ActionEnums.fetch{FeatureName}ByIdError]: (state, { payload }) => ({
      ...state,
      isPending: false,
      error: payload,
    }),

    [{FeatureName}ActionEnums.create{FeatureName}Pending]: (state) => ({
      ...state,
      isPending: true,
      error: null,
    }),
    [{FeatureName}ActionEnums.create{FeatureName}Success]: (state) => ({
      ...state,
      isPending: false,
    }),
    [{FeatureName}ActionEnums.create{FeatureName}Error]: (state, { payload }) => ({
      ...state,
      isPending: false,
      error: payload,
    }),

    [{FeatureName}ActionEnums.update{FeatureName}Pending]: (state) => ({
      ...state,
      isPending: true,
      error: null,
    }),
    [{FeatureName}ActionEnums.update{FeatureName}Success]: (state) => ({
      ...state,
      isPending: false,
    }),
    [{FeatureName}ActionEnums.update{FeatureName}Error]: (state, { payload }) => ({
      ...state,
      isPending: false,
      error: payload,
    }),

    [{FeatureName}ActionEnums.delete{FeatureName}Pending]: (state) => ({
      ...state,
      isPending: true,
      error: null,
    }),
    [{FeatureName}ActionEnums.delete{FeatureName}Success]: (state) => ({
      ...state,
      isPending: false,
    }),
    [{FeatureName}ActionEnums.delete{FeatureName}Error]: (state, { payload }) => ({
      ...state,
      isPending: false,
      error: payload,
    }),

    [{FeatureName}ActionEnums.setFilters]: (state, { payload }) => ({
      ...state,
      filters: { ...state.filters, ...payload, pageNumber: 1 },
    }),
    [{FeatureName}ActionEnums.clearError]: (state) => ({
      ...state,
      error: null,
    }),
  },
  INITIAL_STATE
);
```

---

### File 4: `src/providers/{featureName}Provider/index.tsx`

```typescript
'use client';

import { useMemo, useReducer } from 'react';
import { {FeatureName}StateContext, {FeatureName}ActionContext, INITIAL_STATE } from './context';
import { {featureName}Reducer } from './reducer';
import {
  fetch{FeatureNames}Pending, fetch{FeatureNames}Success, fetch{FeatureNames}Error,
  fetch{FeatureName}ByIdPending, fetch{FeatureName}ByIdSuccess, fetch{FeatureName}ByIdError,
  create{FeatureName}Pending, create{FeatureName}Success, create{FeatureName}Error,
  update{FeatureName}Pending, update{FeatureName}Success, update{FeatureName}Error,
  delete{FeatureName}Pending, delete{FeatureName}Success, delete{FeatureName}Error,
  setFiltersAction, clearErrorAction,
} from './actions';
import {featureName}Service from '@/services/{featureName}Service';

export const {FeatureName}Provider = ({ children }: { children: React.ReactNode }) => {
  const [state, dispatch] = useReducer({featureName}Reducer, INITIAL_STATE);

  const actions = useMemo(() => ({
    fetch{FeatureNames}: async () => {
      dispatch(fetch{FeatureNames}Pending());
      try {
        const response = await {featureName}Service.get{FeatureNames}(state.filters);
        dispatch(fetch{FeatureNames}Success(response));
      } catch (error: any) {
        dispatch(fetch{FeatureNames}Error(error?.message ?? 'Failed to fetch {featureNames}'));
      }
    },

    fetch{FeatureName}ById: async (id: string) => {
      dispatch(fetch{FeatureName}ByIdPending());
      try {
        const response = await {featureName}Service.get{FeatureName}ById(id);
        dispatch(fetch{FeatureName}ByIdSuccess(response));
      } catch (error: any) {
        dispatch(fetch{FeatureName}ByIdError(error?.message ?? 'Failed to fetch {featureName}'));
      }
    },

    create{FeatureName}: async (data: any) => {
      dispatch(create{FeatureName}Pending());
      try {
        await {featureName}Service.create{FeatureName}(data);
        dispatch(create{FeatureName}Success());
      } catch (error: any) {
        dispatch(create{FeatureName}Error(error?.message ?? 'Failed to create {featureName}'));
        throw error;
      }
    },

    update{FeatureName}: async (id: string, data: any) => {
      dispatch(update{FeatureName}Pending());
      try {
        await {featureName}Service.update{FeatureName}(id, data);
        dispatch(update{FeatureName}Success());
      } catch (error: any) {
        dispatch(update{FeatureName}Error(error?.message ?? 'Failed to update {featureName}'));
        throw error;
      }
    },

    delete{FeatureName}: async (id: string) => {
      dispatch(delete{FeatureName}Pending());
      try {
        await {featureName}Service.delete{FeatureName}(id);
        dispatch(delete{FeatureName}Success());
      } catch (error: any) {
        dispatch(delete{FeatureName}Error(error?.message ?? 'Failed to delete {featureName}'));
        throw error;
      }
    },

    setFilters: (filters: any) => dispatch(setFiltersAction(filters)),
    clearError: () => dispatch(clearErrorAction()),
  }), [state.filters]);

  return (
    <{FeatureName}StateContext.Provider value={state}>
      <{FeatureName}ActionContext.Provider value={actions}>
        {children}
      </{FeatureName}ActionContext.Provider>
    </{FeatureName}StateContext.Provider>
  );
};

export { use{FeatureName}, use{FeatureName}Actions } from './context';
```

---

After generating all 4 files, remind the user to:
1. Import and add `{FeatureName}Provider` to `src/providers/index.tsx` (wrap it around the existing children)
2. Export `use{FeatureName}` and `use{FeatureName}Actions` from `src/providers/index.tsx` if an index barrel exists
3. Create the corresponding `src/services/{featureName}Service.ts` (suggest running `/add-service {featureName}`)

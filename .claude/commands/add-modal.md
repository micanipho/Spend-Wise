# Add CRUD Modal

Generate a combined Create/Edit modal component using Ant Design Form. The modal auto-detects create vs edit mode based on whether an item prop is passed.

## Arguments

`$ARGUMENTS` — the feature name (e.g. `invoice`, `lead`, `product`). If not provided, ask the user for it.

## Instructions

Parse `$ARGUMENTS` to extract:
- **featureName**: camelCase (e.g. `invoice`)
- **FeatureName**: PascalCase (e.g. `Invoice`)

Before generating, ask the user:
1. What form fields does this entity have? For each field provide: name, label, type (text input, number, select, textarea, date, switch, checkbox), required?, and any validation rules
2. Are any fields only shown in edit mode (not create)?
3. Are any fields dropdowns that load from another provider? (e.g. a clientId select that loads clients list)
4. Where should this file live? (default: `src/components/{featureName}/{FeatureName}Modal.tsx`)

Then generate the file:

---

### File: `src/components/{featureName}/{FeatureName}Modal.tsx`

```typescript
'use client';

import { useEffect, useState } from 'react';
import { Modal, Form, Input, Button, message } from 'antd';
import { {FeatureName} } from '@/types';
import { use{FeatureName}Actions } from '@/providers/{featureName}Provider';

interface {FeatureName}ModalProps {
  open: boolean;
  onClose: () => void;
  onSuccess?: () => void;
  {featureName}?: {FeatureName} | null;
}

const {FeatureName}Modal = ({ open, onClose, onSuccess, {featureName} }: {FeatureName}ModalProps) => {
  const [form] = Form.useForm();
  const [submitting, setSubmitting] = useState(false);
  const { create{FeatureName}, update{FeatureName} } = use{FeatureName}Actions();

  const isEditing = !!{featureName};

  useEffect(() => {
    if (open) {
      if ({featureName}) {
        form.setFieldsValue({featureName});
      } else {
        form.resetFields();
      }
    }
  }, [open, {featureName}, form]);

  const handleSubmit = async () => {
    try {
      const values = await form.validateFields();
      setSubmitting(true);

      if (isEditing && {featureName}?.id) {
        await update{FeatureName}({featureName}.id, values);
        message.success('{FeatureName} updated successfully');
      } else {
        await create{FeatureName}(values);
        message.success('{FeatureName} created successfully');
      }

      form.resetFields();
      onSuccess?.();
      onClose();
    } catch (error: any) {
      if (error?.errorFields) return; // form validation error, antd handles display
      message.error(error?.message ?? 'Something went wrong');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <Modal
      title={isEditing ? 'Edit {FeatureName}' : 'Create {FeatureName}'}
      open={open}
      onCancel={onClose}
      onOk={handleSubmit}
      okText={isEditing ? 'Save Changes' : 'Create'}
      confirmLoading={submitting}
      destroyOnClose
      width={600}
    >
      <Form form={form} layout="vertical" requiredMark>
        {/* Form fields generated based on user input */}
        {/* Example text field: */}
        {/*
        <Form.Item name="name" label="Name" rules={[{ required: true, message: 'Name is required' }]}>
          <Input placeholder="Enter name" />
        </Form.Item>
        */}

        {/* Example select field: */}
        {/*
        <Form.Item name="status" label="Status" rules={[{ required: true }]}>
          <Select placeholder="Select status">
            <Select.Option value="active">Active</Select.Option>
            <Select.Option value="inactive">Inactive</Select.Option>
          </Select>
        </Form.Item>
        */}

        {/* Edit-only fields wrapped in: isEditing && ( ... ) */}
      </Form>
    </Modal>
  );
};

export default {FeatureName}Modal;
```

Fill in real form fields based on user input, replacing the example comments. Import only the Ant Design form components actually used (Input, Select, DatePicker, InputNumber, Switch, Checkbox, TextArea, etc.).

---

After generating, remind the user to:
1. Load this modal with `next/dynamic` (ssr: false) in the page that uses it
2. Add `{FeatureName}` type fields to `src/types/index.ts` if not already there
3. The modal should be triggered from a list page — run `/add-list-page {featureName}` if needed

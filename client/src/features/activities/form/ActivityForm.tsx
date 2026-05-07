import { Box, Paper, Typography, TextField, Button } from "@mui/material";
import type { FormEvent } from "react";
import { useActivities } from "../../../lib/hooks/useActivities";

type ActivityFormProps = {
  activity?: Activity;
  closeForm: () => void;
};

export default function ActivityForm({
  activity,
  closeForm,
}: ActivityFormProps) {
  const { updateActivity, createActivity } = useActivities();

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const formData = new FormData(event.currentTarget);

    const data: { [key: string]: FormDataEntryValue } = {};
    formData.forEach((value, key) => {
      data[key] = value;
    });

    if (activity) {
      data.id = activity.id;
      await updateActivity.mutateAsync(data as unknown as Activity);
      closeForm();
    } else {
      await createActivity.mutateAsync(data as unknown as Activity);
      closeForm();
    }
  };

  return (
    <Paper sx={{ borderRadius: 3, padding: 3 }}>
      <Typography variant="h5" gutterBottom color="primary">
        Create activity
      </Typography>
      <Box
        component="form"
        sx={{ display: "flex", flexDirection: "column", gap: 3, width: "100%" }}
        onSubmit={handleSubmit}
      >
        <TextField
          name="title"
          label="Title"
          fullWidth
          defaultValue={activity?.title}
        />
        <TextField
          name="description"
          label="Description"
          defaultValue={activity?.description}
          multiline
          rows={3}
          fullWidth
        />
        <TextField
          name="category"
          label="Category"
          fullWidth
          defaultValue={activity?.category}
        />
        <TextField
          name="date"
          label="Date"
          type="date"
          fullWidth
          defaultValue={
            activity?.date
              ? new Date(activity.date).toISOString().split("T")[0]
              : new Date().toISOString().split("T")[0]
          }
        />
        <TextField
          name="city"
          label="City"
          fullWidth
          defaultValue={activity?.city}
        />
        <TextField
          name="venue"
          label="Venue"
          fullWidth
          defaultValue={activity?.venue}
        />
        <Box
          sx={{ display: "flex", justifyContent: "flex-end", gap: 3, pt: 1 }}
        >
          <Button onClick={closeForm} color="inherit">
            Cancel
          </Button>
          <Button
            type="submit"
            color="success"
            variant="contained"
            disabled={updateActivity.isPending || createActivity.isPending}
          >
            Submit
          </Button>
        </Box>
      </Box>
    </Paper>
  );
}
